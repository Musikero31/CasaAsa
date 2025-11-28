using CasaAsa.Business.Component;
using CasaAsa.Business.Component.Authentication;
using CasaAsa.Data;
using CasaAsa.Data.Database;
using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CasaAsaDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));
                        
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public static IServiceCollection AddComponents(this IServiceCollection services)
        {
            services.AddScoped<IAdminComponent, AdminComponent>();

            return services;
        }
    }
}
