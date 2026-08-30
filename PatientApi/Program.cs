using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Repositories;
using PatientApi.Services;

var builder = WebApplication.CreateBuilder(args);

// top of Program.cs
DotNetEnv.Env.Load();


// ---- Services ----

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core + MySQL (Pomelo provider)
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName))
{
    throw new InvalidOperationException("Database connection environment variables are missing.");
}

var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User Id={dbUser};Password={dbPass};";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Dependency injection: repository + service layers
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

var app = builder.Build();

// ---- Middleware pipeline ----

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
