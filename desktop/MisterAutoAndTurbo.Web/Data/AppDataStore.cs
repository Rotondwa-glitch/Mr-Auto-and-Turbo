using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Data
{
    /// <summary>
    /// In-memory data store that syncs with Supabase/PostgreSQL via ApplicationDbContext.
    /// Controllers read/write these lists; PersistChangesFilter handles persistence.
    /// </summary>
    public static class AppDataStore
    {
        public static List<Customer> Customers { get; } = new();
        public static List<Vehicle> Vehicles { get; } = new();
        public static List<Appointment> Appointments { get; } = new();
        public static List<JobCard> JobCards { get; } = new();
        public static List<Quotation> Quotations { get; } = new();
        public static List<Payment> Payments { get; } = new();
        public static List<Inspection> Inspections { get; } = new();
        public static List<Repair> Repairs { get; } = new();
        public static List<Part> Parts { get; } = new();
        public static List<JobPart> JobParts { get; } = new();
        public static List<Complaint> Complaints { get; } = new();
        public static List<ServiceRequest> ServiceRequests { get; } = new();
        public static List<Notification> Notifications { get; } = new();
        public static List<UserAccount> Users { get; } = new();
        public static List<Mechanic> Mechanics { get; } = new();
        public static List<Receptionist> Receptionists { get; } = new();
        public static List<VehicleCollection> VehicleCollections { get; } = new();
        public static List<Pricing> Pricings { get; } = new();
        public static BusinessSettings Settings { get; set; } = new();

        /// <summary>
        /// Called once at startup. Creates tables if needed and loads data into memory.
        /// </summary>
        public static void Initialize(ApplicationDbContext db)
        {
            try
            {
                db.Database.EnsureCreated();
                RefreshFromDatabase(db);

                // Seed a default manager if no users exist
                if (!Users.Any())
                {
                    Users.Add(new UserAccount
                    {
                        Id = Guid.NewGuid().ToString(),
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

        public static void RefreshFromDatabase(ApplicationDbContext db)
        {
            // Simplified: in production this loads all tables into the static lists.
            // Full implementation was in the original AppDataStore.cs (~37 KB).
        }

        public static void SaveToDatabase(ApplicationDbContext db)
        {
            // Simplified: persists changes from the in-memory lists back to Supabase.
            // Full implementation was in the original AppDataStore.cs.
        }

        public static bool ApplyCustomerAppUpdates()
        {
            // Applies changes made by the Flutter customer app (e.g. new bookings).
            return false;
        }
    }
}
