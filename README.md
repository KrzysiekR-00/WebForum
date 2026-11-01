This project is a simple web forum prototype.

# Tech Stack
- C#
- ASP.NET Core (MVC)
- Identity
- EF Core
- SQL Server (LocalDB)

# Architecture

The project follows a standard MVC structure:

- `Areas/Identity/` — Identity pages for login and registration
- `Controllers/` — handles HTTP requests and user actions
- `Data/` — database context
- `Migrations/` — EF Core schema migration history
- `Models/` — domain entities such as `Topic` and `Post`
- `ViewModels/` — DTOs used to pass data between controllers and views
- `Views/` — UI for the forum pages
- `wwwroot/` — static frontend assets