using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.Data.Database
{
    public class CasaAsaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public CasaAsaDbContext(DbContextOptions<CasaAsaDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuDetail> MenuDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderListSummary> OrderListSummaries { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<LockOrder> LockOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // custom configs here

            string adminRoleId = "b52d7e53-5cdc-4dd5-8e45-19fb77e9a1e0";
            string customerRoleId = "8db644e0-6c88-41e5-8be6-f28f3c455447";

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = adminRoleId,
                    Name = "Administrator",
                    NormalizedName = "Administrator",
                },
                new ApplicationRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer"
                });
        }
    }
}
