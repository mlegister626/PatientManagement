# PatientApi

A .NET 8 Web API demonstrating a clean **Entity → DTO → Repository → Service → Controller**
layered architecture, backed by MySQL via EF Core (Pomelo provider). First resource: **Patient**.

## Project layout

```
PatientApi/
├── PatientApi.csproj
├── Program.cs
├── appsettings.json
├── Entities/
│   └── Patient.cs              # EF Core entity (maps to the Patients table)
├── Dtos/
│   ├── PatientDto.cs           # Shape returned on reads
│   ├── CreatePatientDto.cs     # Shape accepted on POST
│   └── UpdatePatientDto.cs     # Shape accepted on PUT
├── Data/
│   └── ApplicationDbContext.cs # EF Core DbContext
├── Repositories/
│   ├── IPatientRepository.cs   # Data access contract (entity-based)
│   └── PatientRepository.cs    # EF Core implementation
├── Services/
│   ├── IPatientService.cs      # Business logic contract (DTO-based)
│   └── PatientService.cs       # Maps entities <-> DTOs, calls repository
└── Controllers/
    └── PatientsController.cs   # REST endpoints
```

**Flow of a request:** Controller (HTTP) → Service (business logic, DTO mapping) →
Repository (data access) → DbContext/EF Core → MySQL.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- A running MySQL server (local install, Docker, etc.)

## 1. Restore packages

From inside the `PatientApi` folder:

```bash
dotnet restore
```

## 2. Configure your connection string

Edit `appsettings.json` and set your real MySQL credentials:

```json
"ConnectionStrings": {
  "PatientDb": "Server=localhost;Port=3306;Database=patient_db;User=root;Password=YOUR_PASSWORD;"
}
```

## 3. Create the database with EF Core migrations

Install the EF Core CLI tool once (if you don't already have it):

```bash
dotnet tool install --global dotnet-ef
```

Then generate and apply the first migration:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This creates the `patient_db` database and the `Patients` table for you automatically.

### Alternative: create the table manually

If you'd rather skip migrations, run this directly in MySQL:

```sql
CREATE DATABASE IF NOT EXISTS patient_db;

USE patient_db;

CREATE TABLE Patients (
    PatientId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName  VARCHAR(100) NOT NULL,
    DOB       DATE NOT NULL,
    Facility  VARCHAR(150) NOT NULL
);
```

## 4. Run the API

```bash
dotnet run
```

By default, ASP.NET Core will print the listening URL (e.g. `https://localhost:5001`).
Navigate to `/swagger` for the interactive API docs.

## 5. Endpoints (CRUD)

| Verb   | Route                | Body               | Description              |
|--------|-----------------------|--------------------|--------------------------|
| GET    | `/api/patients`       | –                  | List all patients        |
| GET    | `/api/patients/{id}`  | –                  | Get one patient by id    |
| POST   | `/api/patients`       | `CreatePatientDto` | Create a new patient     |
| PUT    | `/api/patients/{id}`  | `UpdatePatientDto` | Update an existing patient |
| DELETE | `/api/patients/{id}`  | –                  | Delete a patient         |

### Sample request bodies

**POST /api/patients**
```json
{
  "firstName": "Jane",
  "lastName": "Doe",
  "dob": "1990-05-14",
  "facility": "Main Street Clinic"
}
```

**PUT /api/patients/1**
```json
{
  "firstName": "Jane",
  "lastName": "Smith",
  "dob": "1990-05-14",
  "facility": "Downtown Medical Center"
}
```

## Extending to more entities

This project is intentionally structured so you can repeat the same pattern for new
resources (e.g. `Doctor`, `Appointment`, `Facility`):

1. Add the entity class under `Entities/`.
2. Add `DbSet<T>` to `ApplicationDbContext`.
3. Add DTOs under `Dtos/`.
4. Add `I{Entity}Repository` / `{Entity}Repository` under `Repositories/`.
5. Add `I{Entity}Service` / `{Entity}Service` under `Services/`.
6. Add `{Entity}sController` under `Controllers/`.
7. Register the new repository/service in `Program.cs`.
8. Run `dotnet ef migrations add Add{Entity}` and `dotnet ef database update`.
