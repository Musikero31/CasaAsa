using CasaAsa.Core.Abstraction;
using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.Data.Database
{
    public class CasaAsaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ICurrentUserService _currentUser;

        public CasaAsaDbContext(DbContextOptions<CasaAsaDbContext> options,
                                ICurrentUserService currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

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
                    Value = "Html/ConfirmUser.html",
                    ActiveStatus = true,
                    CreatedBy = Guid.Parse("c325b987-a6ce-4462-9116-f76922e7206c"),
                },
                new ApplicationSetting
                {
                    Id = 2,
                    Category = "Templates",
                    Code = "Reset-Password",
                    Value = "Html/ResetPassword.html",
                    ActiveStatus = true,
                    CreatedBy = Guid.Parse("c325b987-a6ce-4462-9116-f76922e7206c"),
                },
                new ApplicationSetting
                {
                    Id = 3,
                    Category = "Templates",
                    Code = "Customer-Receipt",
                    Value = "Html/CustomerReceipt.html",
                    ActiveStatus = true,
                    CreatedBy = Guid.Parse("c325b987-a6ce-4462-9116-f76922e7206c"),
                });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var now = DateTime.Now;
            var userId = _currentUser.UserId!.Value;

            foreach (var entry in ChangeTracker.Entries<IEntityDefault>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = now;
                        entry.Entity.UpdatedBy = userId;
                        
                        break;
                    case EntityState.Added:
                        entry.Entity.ActiveStatus = true;
                        entry.Entity.CreatedDate = now;
                        entry.Entity.CreatedBy = userId;

                        // Prevent overwriting creation data                        
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.ActiveStatus).IsModified = false;
                        break;
                }
            }
        }
    }
}
