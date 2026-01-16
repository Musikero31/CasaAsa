using CasaAsa.Business.Component;
using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CasaAsa.API.Configuration
{
    public static class SeederConfiguration
    {
        public static async Task SeedDefaultsAsync(IServiceProvider service)
        {
            Console.WriteLine("SeedDefaultsAsync is called....");
            using var scope = service.CreateScope();
            
            await SeedRoles(scope);

            await SeedDefaultUser(scope);
        }

        private static async Task SeedRoles(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                },
                new ApplicationRole
                {
                    Name = "Customer",
                    NormalizedName = "Customer"
                }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.NormalizedName!))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedDefaultUser(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = "admin.user@gmail.com";

            var adminUser = await userManager.FindByNameAsync(adminEmail);

            if (adminUser == null)
            {
                var hashedPassword = new PasswordHasher<ApplicationUser>();
                adminUser = new ApplicationUser
                {
                    Id = Guid.Parse("c325b987-a6ce-4462-9116-f76922e7206c"),
                    FirstName = "Admin",
                    LastName = "User",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    UserName = adminEmail,
                };

                adminUser.PasswordHash = hashedPassword.HashPassword(adminUser, "P@ssw0rd");

                var result = await userManager.CreateAsync(adminUser);

                if (!result.Succeeded)
                {
                    throw new Exception(
                        "Failed to create admin user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    );
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
