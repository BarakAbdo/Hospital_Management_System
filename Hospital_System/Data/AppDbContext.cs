using Hospital_Management_System.Models;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor>Doctors { get; set; }
        public DbSet<Department>Departments { get; set; }
        public DbSet<Appointment>Appointments { get; set; }
        public DbSet<MedicalRecord>MedicalRecords { get; set; }
        public DbSet<Medication>Medications { get; set; }
        public DbSet<Prescription>Prescriptions { get; set; }
        public DbSet<Invoice>Invoices { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PermissionRole> PermissionRoles { get; set; }

        public DbSet<UserRole> RoleUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new
                {
                    pr.RoleId,
                    pr.PermissionId
                });


            modelBuilder.Entity<UserRole>()
                .HasKey(ru => new
                {
                    ru.RoleId,
                    ru.UserId

                });

        }

    }

}
