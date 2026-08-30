using Microsoft.EntityFrameworkCore;
using PatientApi.Entities;
namespace PatientApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Facility> Facilities => Set<Facility>();
        public DbSet<Food> Foods => Set<Food>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");
                entity.HasKey(p => p.PatientId);
                entity.Property(p => p.PatientId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("Facilities");
                entity.HasKey(f => f.FacilityId);
                entity.Property(f => f.FacilityId).ValueGeneratedOnAdd();
                entity.Property(f => f.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");
                entity.HasKey(f => f.FoodId);
                entity.Property(f => f.FoodId).ValueGeneratedOnAdd();
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Calories).IsRequired();
            });
        }
    }
}
