# Revi API - Extended Setup & Next Steps

This scaffold contains a minimal ASP.NET Core (NET 6) Web API with models and controller stubs for:
- Authentication (Identity-based register/login endpoints)
- Courses, Batches, Enrollments
- Assignments and Submissions (versioning placeholder)
- Reviews (peer review model)
- Discussions and Comments

Important: This environment where I created files does not have the `dotnet` CLI available, so I could not run `dotnet restore`/`dotnet build`/`dotnet ef` here. Follow the steps below on your machine.

## Prerequisites
- .NET 6 SDK installed
- (Optional) `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`
- Recommended VS Code extension: `ms-dotnettools.csharp`

## Local setup (PowerShell)
```powershell
cd c:\Users\munis\review1\revi
# restore packages
cd src\Revi.Api
dotnet restore
# create database and run migrations (if you want EF migrations):
# dotnet ef migrations add Initial
# dotnet ef database update
# build and run
dotnet build
dotnet run
```

The API listens on the default Kestrel ports. Use `/weatherforecast` and `/auth/register`, `/auth/login` sample endpoints.

## Git / GitHub
I did not push to your repository automatically.
To push these files to `https://github.com/KL2400032365/Thamo.git` do:

```powershell
cd c:\Users\munis\review1\revi
git init
git remote add origin https://github.com/KL2400032365/Thamo.git
git add .
git commit -m "Scaffold: initial .NET 6 Revi API with models/controllers"
# If your remote requires authentication, use credential helper or set up PAT
git push -u origin main
```

If you'd like, I can attempt the git push from this environment — prompt me and confirm you want me to try (I may need authentication).

## Next recommended work (I can do these next)
- Implement JWT generation on login and secure endpoints
- Implement file upload handling (store files to disk or cloud)
- Add allocation algorithm for peer review assignments
- Add admin endpoints and role-based authorization
- Add unit tests and integration tests
- Add CI pipeline and optional Dockerfile

Tell me which item you'd like me to implement next or if you want me to try pushing to GitHub now.
