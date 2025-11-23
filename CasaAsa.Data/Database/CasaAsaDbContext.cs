using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.Data.Database
{
    public class CasaAsaDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public CasaAsaDbContext(DbContextOptions<CasaAsaDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // custom configs here
        }
    }
}
