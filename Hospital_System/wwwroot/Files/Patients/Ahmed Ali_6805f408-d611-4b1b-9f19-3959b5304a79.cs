using First_ASP_Task.Models;
using Microsoft.EntityFrameworkCore;
using Second_ASP_EF_MVC.Models;


namespace First_ASP_Task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<UserFile> UserFiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new
                {
                    pr.RolesId,
                    pr.PemissionsId
                });


            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new
                {
                    ru.RoleId,
                    ru.UserId

                });

        }

    }
}
