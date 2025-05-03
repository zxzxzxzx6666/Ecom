using ApplicationCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Queries;
using Infrastructure.Logging;
namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        // register application core
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration) 
        {
            // register EfRepository
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // register IdentityService
            services.AddScoped<IIdentityService, IdentityService>();

            // register 
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketQueryService, BasketQueryService>();

            var catalogSettings = configuration.Get<CatalogSettings>() ?? new CatalogSettings();
            services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

            // register Infratructure logging
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
