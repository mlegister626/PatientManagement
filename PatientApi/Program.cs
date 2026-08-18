using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Repositories;
using PatientApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core + MySQL (Pomelo provider)
var connectionString = builder.Configuration.GetConnectionString("PatientDb")
    ?? throw new InvalidOperationException("Connection string 'PatientDb' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Dependency injection: repository + service layers
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();

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
