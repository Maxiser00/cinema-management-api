# Cinema Management API

A RESTful API built with **ASP.NET Core 8**, **Entity Framework Core**, and **Microsoft SQL Server** for managing a relational cinema database.

The application provides endpoints for managing movies, actors, directors, studios, categories, languages, and countries, including their relationships.

## Features

- **Movie management:** Create, retrieve, update, and delete movies.
- **Actor and director management:** Manage people and their biographical information.
- **Movie relationships:** Associate movies with actors, directors, categories, and countries.
- **Reference data management:** Manage studios, languages, categories, and countries.
- **Detailed API responses:** Retrieve movies and related information through DTO-based projections.
- **Database migrations:** Maintain the relational SQL Server schema using Entity Framework Core.
- **Swagger documentation:** Explore and test the API through Swagger UI in development mode.

## Tech Stack

| Technology              | Purpose                          |
| ----------------------- | -------------------------------- |
| C#                      | Programming language             |
| ASP.NET Core 8          | REST API framework               |
| Entity Framework Core 8 | Object-relational mapping        |
| Microsoft SQL Server    | Relational database              |
| LINQ                    | Database queries and projections |
| Swagger / Swashbuckle   | Interactive API documentation    |

## Project Structure

```text
CinemaAPI/
├── Controllers/        # HTTP endpoints
├── DTO/                # Data transfer objects
├── Models/             # Domain entities and relationships
├── Migrations/         # Entity Framework Core migrations
├── CinemaContext.cs    # Database context
├── Program.cs          # Application configuration
└── CinemaAPI.csproj
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Microsoft SQL Server or SQL Server Express
- Entity Framework Core CLI tools (8.x)

### 1. Clone the repository

```bash
git clone https://github.com/Maxiser00/cinema-management-api.git
cd cinema-management-api/CinemaAPI
```

### 2. Configure the database

The application uses a connection string named `CinemaDatabase`.

For local development, configure it with .NET User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:CinemaDatabase" "Server=localhost\SQLEXPRESS;Database=CinemaDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

Adjust the SQL Server instance name and authentication settings according to your environment.

Do not commit database passwords or other credentials.

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Create the database

Install Entity Framework Core CLI tools if needed:

```bash
dotnet tool install --global dotnet-ef --version 8.0.8
```

Apply the existing migrations:

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run --launch-profile https
```

Open the Swagger UI at the HTTPS address displayed in the terminal, followed by `/swagger`.

For example:

```text
https://localhost:<port>/swagger
```

Swagger UI is available in the development environment.

## API Resources

The API exposes the following resources:

| Resource   | Base Endpoint   |
| ---------- | --------------- |
| Movies     | `/api/Movie`    |
| Actors     | `/api/Actor`    |
| Directors  | `/api/Director` |
| Studios    | `/api/Studio`   |
| Categories | `/api/Category` |
| Languages  | `/api/Language` |
| Countries  | `/api/Country`  |

The API supports common CRUD operations as well as endpoints for managing relationships between movies and associated entities.

Consult Swagger UI for available operations, request bodies, and response formats.

## Database Design

The database includes entities for movies, people, studios, languages, categories, and countries.

Many-to-many relationships are represented through dedicated association entities:

- `MovieActor`
- `MovieDirector`
- `MovieCategory`
- `MovieCountry`

Entity Framework Core migrations are included to recreate the database schema.

## Project Status

**Educational project — functional backend demonstration.**

The API has been tested locally using SQL Server Express and Swagger. Core CRUD operations and movie relationships have been exercised.

This project is intended to demonstrate backend development, REST API design, relational data modeling, and Entity Framework Core.

## Author

**Maxiser00**

GitHub: [@Maxiser00](https://github.com/Maxiser00)
