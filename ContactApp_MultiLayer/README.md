# ContactApp - Multi-layered Solution

Projects:
- ContactApp.Domain (domain entities)
- ContactApp.Application (interfaces, DTOs, application services)
- ContactApp.Infrastructure (EF Core, repository implementations, DI)
- ContactApp.Api (ASP.NET Core Web API)

How to run:
1. Install .NET 8 SDK (preview/RC if needed) or change TargetFramework to net7.0.
2. From the repository root run:
   dotnet restore
   dotnet build
   dotnet run --project src/ContactApp.Api

The API will create a SQLite database file `contacts.db` automatically using the connection string in appsettings.json.
