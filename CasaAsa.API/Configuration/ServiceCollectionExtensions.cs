using CasaAsa.Business.Component.Administration;
using CasaAsa.Business.Component.Administration.Authentication;
using CasaAsa.Business.Component.Configuration;
using CasaAsa.Core.Abstraction;
using CasaAsa.Data.Database;
using CasaAsa.Data.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CasaAsa.API.Configuration
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

        public static IServiceCollection AddBusinessComponents(this IServiceCollection services)
        {
            services.AddScoped<IAdminComponent, AdminComponent>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAddressComponent, AddressComponent>();

            return services;
        }

        public static IServiceCollection AddConfigurationsComponents(this IServiceCollection services)
        {
            services.AddScoped<IHtmlParser, HtmlParser>();
            services.AddScoped<ISettingsComponent, SettingsComponent>();
            services.AddScoped<IMailComponent, MailComponent>();

            return services;
        }

    }
}
