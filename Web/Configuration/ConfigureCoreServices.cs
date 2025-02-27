using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Data;
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
            return services;
        }
    }
}
