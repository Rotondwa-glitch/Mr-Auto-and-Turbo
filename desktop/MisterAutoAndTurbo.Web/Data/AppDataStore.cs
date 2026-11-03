using Microsoft.EntityFrameworkCore;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Data
{
    /// <summary>
    /// Working copy of the staff app data. Loaded from Supabase (PostgreSQL) on startup
    /// and written back after every change (see PersistChangesFilter).
    /// Restored from the original AppDataStore implementation.
    /// </summary>
    public static class AppDataStore
    {
        private static readonly object _lock = new();
        private static readonly object _dbLock = new();
        private static int _jobSeq = 3;

        private static readonly TimeSpan RefreshInterval = TimeSpan.FromSeconds(2.5);
        private static DateTime _lastRefreshUtc = DateTime.MinValue;
        private static DateTime _lastFullLoadUtc = DateTime.MinValue;
        private static readonly TimeSpan FullLoadInterval = TimeSpan.FromSeconds(30);

        private static readonly Dictionary<Type, HashSet<string>> _known = new();

        private static string? IdOf<T>(T e) => (string?)typeof(T).GetProperty("Id")?.GetValue(e);

        private static void RememberKnown<T>(IEnumerable<T> items) where T : class
        {
            var set = _known.GetValueOrDefault(typeof(T)) ?? new HashSet<string>();
            foreach (var item in items)
            {
                var id = IdOf(item);
                if (!string.IsNullOrEmpty(id)) set.Add(id);
            }
            _known[typeof(T)] = set;
        }

        public static List<Vehicle> Vehicles { get; } = new();
        public static List<Customer> Customers { get; } = new();
        public static List<JobCard> JobCards { get; } = new();
        public static List<ServiceRequest> ServiceRequests { get; } = new();
        public static List<Quotation> Quotations { get; } = new();
        public static List<Appointment> Appointments { get; } = new();
        public static List<Part> Parts { get; } = new();
        public static List<Payment> Payments { get; } = new();
        public static List<VehicleCollection> VehicleCollections { get; } = new();
        public static List<Complaint> Complaints { get; } = new();
        public static List<Notification> Notifications { get; } = new();
        public static List<Inspection> Inspections { get; } = new();
        public static List<Repair> Repairs { get; } = new();
        public static List<JobPart> JobParts { get; } = new();
        public static List<UserAccount> Users { get; } = new();
        public static List<Mechanic> Mechanics { get; } = new();
        public static List<Receptionist> Receptionists { get; } = new();
        public static List<Pricing> Pricings { get; } = new();
        public static BusinessSettings Settings { get; set; } = new();

        public static void Initialize(ApplicationDbContext db)
        {
            try
            {
                db.Database.EnsureCreated();
                LoadFromDatabase(db);

                if (!Users.Any())
                {
                    Users.Add(new UserAccount
                    {
                        Id = NewId("usr"),
                        FullName = "Workshop Manager",
                        Email = "manager@misterauto.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                        Role = UserRoles.Manager,
                        CreatedAt = DateTime.UtcNow
                    });
                    SaveToDatabase(db);
                }

                if (Settings.Id == 0)
                {
                    Settings = new BusinessSettings { Id = 1 };
                    SaveToDatabase(db);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AppDataStore.Initialize warning: {ex.Message}");
            }
        }

        public static void LoadFromDatabase(ApplicationDbContext db)
        {
            lock (_dbLock)
            {
                LoadFromDatabaseUnlocked(db);
                _lastFullLoadUtc = DateTime.UtcNow;
                _lastRefreshUtc = _lastFullLoadUtc;
            }
        }

        private static void LoadFromDatabaseUnlocked(ApplicationDbContext db)
        {
            Replace(Customers, db.Set<Customer>());
            Replace(Vehicles, db.Set<Vehicle>());
            Replace(JobCards, db.Set<JobCard>());
            Replace(ServiceRequests, db.Set<ServiceRequest>());
            Replace(Quotations, db.Set<Quotation>());
            Replace(Appointments, db.Set<Appointment>());
            Replace(Parts, db.Set<Part>());
            Replace(Payments, db.Set<Payment>());
            Replace(VehicleCollections, db.Set<VehicleCollection>());
            Replace(Complaints, db.Set<Complaint>());
            Replace(Notifications, db.Set<Notification>());
            Replace(Inspections, db.Set<Inspection>());
            Replace(Repairs, db.Set<Repair>());
            Replace(JobParts, db.Set<JobPart>());
            Replace(Users, db.Set<UserAccount>());
            Replace(Mechanics, db.Set<Mechanic>());
            Replace(Receptionists, db.Set<Receptionist>());
            Replace(Pricings, db.Set<Pricing>());

            var settings = db.Set<BusinessSettings>().AsNoTracking().FirstOrDefault();
            if (settings != null) Settings = settings;

            RememberKnown(Customers);
            RememberKnown(Vehicles);
            RememberKnown(JobCards);
            RememberKnown(Appointments);
            RememberKnown(Quotations);
            RememberKnown(Payments);
            RememberKnown(Complaints);
            RememberKnown(Notifications);
            RememberKnown(Users);
            RememberKnown(Parts);
            RememberKnown(Inspections);
            RememberKnown(VehicleCollections);
            RememberKnown(Pricings);
        }

        public static void MergeExternalChanges(ApplicationDbContext db)
        {
            lock (_dbLock)
            {
                MergeMissing(Customers, db.Set<Customer>());
                MergeMissing(Vehicles, db.Set<Vehicle>());
                MergeMissing(Appointments, db.Set<Appointment>());
                MergeMissing(ServiceRequests, db.Set<ServiceRequest>());
                MergeMissing(Complaints, db.Set<Complaint>());
                MergeMissing(Payments, db.Set<Payment>());
            }
        }

        public static void ReloadFromDatabase(ApplicationDbContext db)
        {
            LoadFromDatabase(db);
        }

        public static void RefreshFromDatabase(ApplicationDbContext db, bool force = false)
        {
            lock (_dbLock)
            {
                var now = DateTime.UtcNow;
                if (!force && now - _lastRefreshUtc < RefreshInterval) return;

                if (force || now - _lastFullLoadUtc >= FullLoadInterval)
                {
                    LoadFromDatabaseUnlocked(db);
                    _lastFullLoadUtc = now;
                }
                else
                {
                    // Fast path: only tables the Flutter app can change
                    MergeMissing(Customers, db.Set<Customer>());
                    MergeMissing(Vehicles, db.Set<Vehicle>());
                    MergeMissing(Appointments, db.Set<Appointment>());
                    MergeMissing(ServiceRequests, db.Set<ServiceRequest>());
                    MergeMissing(Complaints, db.Set<Complaint>());
                    MergeMissing(Payments, db.Set<Payment>());
                }
                _lastRefreshUtc = now;
            }
        }

        /// <summary>
        /// Follow-up for actions the customer took in the Flutter app.
        /// Creates staff notifications for new bookings/complaints. Idempotent.
        /// </summary>
        public static bool ApplyCustomerAppUpdates()
        {
            var changed = false;

            foreach (var appt in Appointments.Where(a => a.Status == "Pending" || a.Status == "Scheduled"))
            {
                var key = $"booking:{appt.Id}";
                if (Notifications.Any(n => n.Type == "booking" && n.RelatedId == appt.Id)) continue;

                Notifications.Add(new Notification
                {
                    Id = NewId("ntf"),
                    Title = "New booking",
                    Message = $"Appointment scheduled for {appt.ScheduledAt:g}",
                    Type = "booking",
                    RelatedId = appt.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
                changed = true;
            }

            foreach (var c in Complaints.Where(x => x.Status == "Open"))
            {
                if (Notifications.Any(n => n.Type == "complaint" && n.RelatedId == c.Id)) continue;

                Notifications.Add(new Notification
                {
                    Id = NewId("ntf"),
                    Title = "New complaint",
                    Message = c.Subject,
                    Type = "complaint",
                    RelatedId = c.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
                changed = true;
            }

            return changed;
        }

        public static void SaveToDatabase(ApplicationDbContext db)
        {
            lock (_dbLock)
            {
                Sync(db, db.Set<Customer>(), Customers);
                Sync(db, db.Set<Vehicle>(), Vehicles);
                Sync(db, db.Set<JobCard>(), JobCards);
                Sync(db, db.Set<ServiceRequest>(), ServiceRequests);
                Sync(db, db.Set<Quotation>(), Quotations);
                Sync(db, db.Set<Appointment>(), Appointments);
                Sync(db, db.Set<Part>(), Parts);
                Sync(db, db.Set<Payment>(), Payments);
                Sync(db, db.Set<VehicleCollection>(), VehicleCollections);
                Sync(db, db.Set<Complaint>(), Complaints);
                Sync(db, db.Set<Notification>(), Notifications);
                Sync(db, db.Set<Inspection>(), Inspections);
                Sync(db, db.Set<Repair>(), Repairs);
                Sync(db, db.Set<JobPart>(), JobParts);
                Sync(db, db.Set<UserAccount>(), Users);
                Sync(db, db.Set<Mechanic>(), Mechanics);
                Sync(db, db.Set<Receptionist>(), Receptionists);
                Sync(db, db.Set<Pricing>(), Pricings);

                var settingsSet = db.Set<BusinessSettings>();
                var existing = settingsSet.Find(Settings.Id);
                if (existing == null)
                {
                    if (Settings.Id == 0) Settings.Id = 1;
                    settingsSet.Add(Settings);
                }
                else
                {
                    db.Entry(existing).CurrentValues.SetValues(Settings);
                }

                db.SaveChanges();

                RememberKnown(Customers);
                RememberKnown(Vehicles);
                RememberKnown(JobCards);
                RememberKnown(Appointments);
                RememberKnown(Quotations);
                RememberKnown(Payments);
                RememberKnown(Complaints);
                RememberKnown(Notifications);
                RememberKnown(Users);
                RememberKnown(Parts);
                RememberKnown(Inspections);
                RememberKnown(VehicleCollections);
                RememberKnown(Pricings);
            }
        }

        private static void Replace<T>(List<T> list, IQueryable<T> query) where T : class
        {
            list.Clear();
            list.AddRange(query.AsNoTracking().ToList());
        }

        private static void MergeMissing<T>(List<T> list, IQueryable<T> query) where T : class
        {
            var known = _known.GetValueOrDefault(typeof(T)) ?? new HashSet<string>();
            var inMemory = new HashSet<string>(list.Select(IdOf).Where(id => !string.IsNullOrEmpty(id))!);

            foreach (var row in query.AsNoTracking().ToList())
            {
                var id = IdOf(row);
                if (string.IsNullOrEmpty(id)) continue;
                if (inMemory.Contains(id)) continue;
                // Do not re-add rows we previously knew and that were deleted locally
                if (known.Contains(id)) continue;

                list.Add(row);
                known.Add(id);
            }
            _known[typeof(T)] = known;
        }

        private static void Sync<T>(ApplicationDbContext db, DbSet<T> set, List<T> items) where T : class
        {
            var key = db.Model.FindEntityType(typeof(T))!.FindPrimaryKey()!.Properties[0].PropertyInfo!;
            var saved = set.AsNoTracking().ToList().ToDictionary(e => (string)key.GetValue(e)!);
            var seen = new HashSet<string>();

            foreach (var item in items)
            {
                var id = (string?)key.GetValue(item);
                if (string.IsNullOrEmpty(id) || !seen.Add(id)) continue;

                if (saved.TryGetValue(id, out var row))
                {
                    set.Attach(row);
                    var entry = db.Entry(row);
                    entry.CurrentValues.SetValues(item);
                    if (!entry.Properties.Any(p => p.IsModified))
                        entry.State = EntityState.Unchanged;
                }
                else
                {
                    set.Add(item);
                }
            }

            foreach (var kv in saved)
            {
                if (seen.Contains(kv.Key)) continue;
                var stub = kv.Value;
                set.Attach(stub);
                set.Remove(stub);
            }
        }

        public static string NewId(string prefix)
        {
            lock (_lock)
            {
                return $"{prefix}-{Guid.NewGuid().ToString("N")[..8]}";
            }
        }

        public static string NextJobCardId()
        {
            lock (_lock)
            {
                _jobSeq++;
                return $"job-{_jobSeq:D3}";
            }
        }
    }
}
