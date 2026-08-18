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
        }
    }
}
