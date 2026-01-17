using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.Data.Database
{
    public class CasaAsaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
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

            builder.Entity<ApplicationSetting>().HasData(
                new ApplicationSetting
                {
                    Id = 1,
                    Category = "Templates",
                    Code = "Confirm-Email",
                    Value = "Html/ConfirmUser.html"
                },
                new ApplicationSetting
                {
                    Id = 2,
                    Category = "Templates",
                    Code = "Reset-Password",
                    Value = "Html/ResetPassword.html"
                },
                new ApplicationSetting
                {
                    Id = 3,
                    Category = "Templates",
                    Code = "Customer-Receipt",
                    Value = "Html/CustomerReceipt.html"
                });
        }
    }
}
