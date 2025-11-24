using CasaAsa.Data.Database;
using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CasaAsa.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAndIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CasaAsaDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<CasaAsaDbContext>()
            .AddDefaultTokenProviders();

            //services.AddScoped<IJwtTokenService, JwtTokenService>();
            //services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
