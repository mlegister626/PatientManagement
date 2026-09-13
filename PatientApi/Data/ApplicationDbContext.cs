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
        public DbSet<MealDelivery> MealDeliveries => Set<MealDelivery>();
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

            modelBuilder.Entity<MealDelivery>(entity =>
            {
                entity.ToTable("MealDeliveries");
                entity.HasKey(m => m.MealDeliveryId);
                entity.Property(m => m.MealDeliveryId).ValueGeneratedOnAdd();

                entity.Property(m => m.PortionGiven)
                    .IsRequired()
                    .HasColumnType("double");

                entity.Property(m => m.DateDelivered)
                    .IsRequired()
                    .HasColumnType("date");


                entity.Property(m => m.MealType)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.HasOne(m => m.Patient)
                    .WithMany()
                    .HasForeignKey(m => m.PatientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                // Restrict (not Cascade) on purpose: deleting a Food item
                // shouldn't silently wipe historical delivery records.
                entity.HasOne(m => m.Food)
                    .WithMany()
                    .HasForeignKey(m => m.FoodId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

        }
    }
}
