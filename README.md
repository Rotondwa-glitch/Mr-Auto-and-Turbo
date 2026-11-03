# Mister Auto & Turbo – VSMS

Vehicle Service Management System for **Mister Auto & Turbo** (Welkom, Free State).

Academic WIL project by **Team Web Vortex**.

> **Quick start:** see **[SETUP.md](SETUP.md)** for clone-and-run steps.

## Run after download

### Desktop (staff web app)
```bash
cd desktop/MisterAutoAndTurbo.Web
dotnet restore
dotnet run
```
Login: `manager@misterauto.local` / `Manager123!`  
Uses **SQLite** by default (`misterauto.db`) — no cloud DB required.

### Mobile (customer app)
```bash
cd mobile/mister_auto_and_turbo
flutter create . --project-name mister_auto_and_turbo
flutter pub get
flutter run
```
Without Supabase configured, any email/password works in **demo mode** so you can navigate the UI.

## Repository structure
```
desktop/MisterAutoAndTurbo.Web/   # ASP.NET Core staff app
mobile/mister_auto_and_turbo/     # Flutter customer app
SETUP.md                          # Detailed run instructions
```

## Technologies
- **Desktop:** ASP.NET Core 8, EF Core, SQLite (default) or PostgreSQL/Supabase
- **Mobile:** Flutter, Provider, Supabase (optional)

## Team Web Vortex
- LE Letsie · RG Madi · NM Netshituka · LE Lipali · LTBG Pule

**Status:** Core migration complete — apps run after clone
