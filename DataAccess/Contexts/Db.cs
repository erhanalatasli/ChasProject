using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<DoctorPatient> DoctorPatients { get; set; }

        public DbSet<Clinic> Clinics { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorPatient>().HasKey(dp => new { dp.DoctorId, dp.PatientId });
            modelBuilder.Entity<DoctorPatient>()
            .HasOne(dp => dp.Patient)
            .WithMany(p => p.DoctorPatients)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hospital>()
            .HasOne(h => h.City)
            .WithMany(c => c.Hospitals)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
            .HasMany(c => c.Hospitals)
            .WithOne(h => h.City)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<District>()
            .HasMany(d => d.Hospitals)
            .WithOne(h => h.District)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
