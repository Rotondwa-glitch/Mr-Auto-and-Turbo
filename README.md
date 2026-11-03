# Mister Auto & Turbo – VSMS

Vehicle Service Management System for **Mister Auto & Turbo** (Welkom, Free State).

Academic WIL project by **Team Web Vortex**.

## Repository Structure

```
Mr-Auto-and-Turbo/
├── desktop/                          # ASP.NET Core staff / workshop web app
│   └── MisterAutoAndTurbo.Web/
│       ├── Controllers/              # All staff controllers
│       ├── Models/                   # Domain models
│       ├── Data/                     # AppDataStore, DbContext, filters
│       ├── Services/                 # Email, password, SA time
│       ├── ViewModels/
│       ├── Views/                    # Razor UI (auth, dashboards, CRUD)
│       ├── wwwroot/css|js            # Site styles & scripts
│       ├── Program.cs
│       └── ...
├── mobile/                           # Flutter customer mobile app
│   └── mister_auto_and_turbo/
│       ├── lib/
│       │   ├── main.dart
│       │   ├── config/
│       │   ├── models/
│       │   ├── views/                # All customer screens
│       │   ├── viewmodels/
│       │   ├── repositories/
│       │   ├── services/
│       │   ├── routes/
│       │   └── widgets/
│       └── pubspec.yaml
├── README.md
└── .gitignore
```

## Technologies
- **Desktop**: ASP.NET Core 8 / C# / Entity Framework Core / PostgreSQL (Supabase)
- **Mobile**: Flutter / Dart / Provider / Supabase

## User Roles
- Customer (mobile app)
- Receptionist
- Mechanic
- Manager

## Main Features
- Customer & vehicle registration
- Digital job cards & inspections
- Repair status tracking
- Quotations & approvals
- Payment recording
- Role-based dashboards
- Notifications (staff + customer)
- Pricing / parts inventory

## Getting Started

### Desktop (ASP.NET)
```bash
cd desktop/MisterAutoAndTurbo.Web
dotnet restore
# Configure ConnectionStrings:Supabase in appsettings.json or User Secrets
dotnet run
```

Default seeded manager (when DB is empty):
- Email: `manager@misterauto.local`
- Password: `Manager123!`

### Mobile (Flutter)
```bash
cd mobile/mister_auto_and_turbo
flutter pub get
# Set Supabase URL + anon key in lib/config/supabase_config.dart
# Generate platform folders if needed:
#   flutter create .
flutter run
```

## Import Status
Source migrated from `Inheritancersa/MisterAutoAndTurbo` into this structured layout.

**Included:** full app source for desktop + mobile (controllers, models, data layer, views, Flutter screens).

**Excluded (large binaries):**
- Full project zip archives (~14.5 MB each)
- Large SRS PDF (~12.8 MB)
- Large architectural diagram images
- Bootstrap `wwwroot/lib` vendor tree / source maps
- Logo/favicon binary assets (optional to re-add)

## Team Web Vortex
- LE Letsie
- RG Madi
- NM Netshituka
- LE Lipali
- LTBG Pule

**Status:** Core migration complete — ready for local run & further development
