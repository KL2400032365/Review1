<<<<<<< HEAD
```markdown
# Review1 — Revi API (Minimal .NET Web API)

This workspace contains a minimal ASP.NET Core Web API and companion static UI used for a peer-review and collaboration demo.

## Build and run (PowerShell)

```powershell
cd src\Revi.Api
dotnet restore
dotnet build
dotnet run --urls "http://localhost:5000"
```

The API exposes sample endpoints and serves a simple static UI under `wwwroot`:
- `GET /weatherforecast` — sample payload
- `GET /index.html` — demo login UI (auto-seed for test user)

## Recommended VS Code extension
- C# (ms-dotnettools.csharp)

Install it from the terminal:

```powershell
code --install-extension ms-dotnettools.csharp
```

## Notes
- Project was originally targeted at `net6.0`; depending on your local SDK you may retarget to a newer framework or install the .NET 6 runtime.
- Use `dotnet restore` before `dotnet build` when dependencies change.

```
