# Mister Auto & Turbo – VSMS

Vehicle Service Management System for **Mister Auto & Turbo** (Welkom, Free State).

Academic WIL project by **Team Web Vortex**.

## Repository Structure

```
Mr-Auto-and-Turbo/
├── desktop/                          # ASP.NET Core staff / workshop web app
│   └── MisterAutoAndTurbo.Web/
│       ├── Controllers/
│       ├── Models/
│       ├── Data/
│       ├── Services/
│       ├── ViewModels/
│       ├── Views/                    # (partial)
│       ├── Program.cs
│       └── ...
├── mobile/                           # Flutter customer mobile app
│   └── mister_auto_and_turbo/
│       ├── lib/
│       │   ├── main.dart
│       │   ├── models/
│       │   ├── views/                # (continuing)
│       │   ├── viewmodels/
│       │   └── ...
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
- Digital job cards
- Repair status tracking
- Quotations & approvals
- Payment recording
- Role-based dashboards
- Notifications

## Getting Started

### Desktop (ASP.NET)
```bash
cd desktop/MisterAutoAndTurbo.Web
dotnet restore
# Configure ConnectionStrings:Supabase in appsettings.json or User Secrets
dotnet run
```

### Mobile (Flutter)
```bash
cd mobile/mister_auto_and_turbo
flutter pub get
# Set Supabase URL + anon key in lib/config/supabase_config.dart
flutter run
```

## Import Status
Core source code imported from `Inheritancersa/MisterAutoAndTurbo`.

**Excluded (large binaries):**
- Full project zip archives (~14.5 MB each)
- Large SRS PDF (~12.8 MB)
- Large architectural diagram images
- Bootstrap source maps and heavy image assets

## Team Web Vortex
- LE Letsie
- RG Madi
- NM Netshituka
- LE Lipali
- LTBG Pule

**Status:** In Development
