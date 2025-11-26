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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // custom configs here
        }
    }
}
