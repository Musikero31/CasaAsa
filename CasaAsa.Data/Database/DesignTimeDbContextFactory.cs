using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CasaAsa.Data.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CasaAsaDbContext>
    {
        public CasaAsaDbContext CreateDbContext(string[] args)
        {
            var userSecretsKey = "010dcc9c-89be-40a5-b0a1-c4bdfdc3c278";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(userSecretsKey)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CasaAsaDbContext>();
            Console.WriteLine($"Connection string value: {configuration.GetConnectionString("DefaultConnection")}");

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new CasaAsaDbContext(optionsBuilder.Options);
        }
    }
}
