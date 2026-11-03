# Contributing & branch strategy

## Long-lived branches

| Branch | Purpose |
|--------|--------|
| `main` | Stable, runnable release |
| `develop` | Integration branch for the next release |

## Feature branches (reuse this pattern)

| Branch | Purpose |
|--------|--------|
| `feature/flutter-neogradient` | Flutter customer app UI (NeoGradient) |
| `feature/desktop-staff` | ASP.NET staff web app |
| `feature/<name>` | New work — create from `develop` |

### How to add a new feature

```bash
git fetch origin
git checkout develop
git pull origin develop
git checkout -b feature/my-feature
# ... work ...
git push -u origin feature/my-feature
# Open a Pull Request into develop
```

### Release

```bash
# After QA on develop
git checkout main
git merge develop
git push origin main
```

Do **not** commit secrets (Supabase service role keys, SMTP passwords).
