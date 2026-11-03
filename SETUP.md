# How to run Mister Auto & Turbo

After cloning **https://github.com/Rotondwa-glitch/Mr-Auto-and-Turbo**:

---

## 1. Desktop staff app (ASP.NET)

**Requirements:** [.NET 8 SDK](https://dotnet.microsoft.com/download)

```bash
cd desktop/MisterAutoAndTurbo.Web
dotnet restore
dotnet run
```

Open the URL shown in the terminal (usually `https://localhost:7xxx` or `http://localhost:5xxx`).

**Demo login (seeded on first run):**
- Email: `manager@misterauto.local`
- Password: `Manager123!`

By default the app uses a **local SQLite** file (`misterauto.db`) — no Supabase required.

### Optional: use Supabase/PostgreSQL

In `appsettings.json` set:

```json
"ConnectionStrings": {
  "Supabase": "Host=db.xxxx.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=...;SSL Mode=Require"
}
```

If `Supabase` is empty or still contains `YOUR_`, SQLite is used automatically.

---

## 2. Mobile customer app (Flutter)

**Requirements:** [Flutter SDK](https://docs.flutter.dev/get-started/install) (3.22+)

```bash
cd mobile/mister_auto_and_turbo
flutter create . --project-name mister_auto_and_turbo
flutter pub get
flutter run
```

`flutter create .` generates `android/`, `ios/`, etc. without overwriting `lib/`.

**Demo mode:** if Supabase is not configured, any non-empty email/password signs you in so you can click through the UI offline.

### Optional: live Supabase

Edit `lib/config/supabase_config.dart`:

```dart
static const String url = 'https://YOUR_PROJECT.supabase.co';
static const String anonKey = 'YOUR_ANON_KEY';
```

---

## Troubleshooting

| Issue | Fix |
|--------|-----|
| `dotnet` not found | Install .NET 8 SDK |
| `flutter` not found | Install Flutter and add to PATH |
| HTTPS certificate warning | Use the `http://` URL from `dotnet run` output |
| Port in use | `dotnet run --urls http://localhost:5080` |
| Flutter platform folders missing | Run `flutter create .` in the mobile project folder |
