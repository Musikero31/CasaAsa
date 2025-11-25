using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CasaAsa.Data.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CasaAsaDbContext>
    {
        public CasaAsaDbContext CreateDbContext(string[] args)
        {
            // Put a connection string here ONLY for design-time (migrations)
            const string designTimeConnectionString =
                "Server=tcp:casaasa-az-sql.database.windows.net,1433;Initial Catalog=CasaAsaDb;Persist Security Info=False;User ID=sqlazadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=0;";

            var optionsBuilder = new DbContextOptionsBuilder<CasaAsaDbContext>();
            optionsBuilder.UseSqlServer(designTimeConnectionString);

            return new CasaAsaDbContext(optionsBuilder.Options);
        }

        //public CasaAsaDbContext CreateDbContext(string[] args)
        //{
        //    // Load *local* config for design time only
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())  // this now points to CasaAsa.Data
        //        .AddJsonFile("appsettings.json", optional: false)
        //        .Build();

        //    var connectionString = config.GetConnectionString("DefaultConnection");

        //    var optionsBuilder = new DbContextOptionsBuilder<CasaAsaDbContext>();
        //    optionsBuilder.UseSqlServer(connectionString);

        //    return new CasaAsaDbContext(optionsBuilder.Options);
        //}
    }
}
