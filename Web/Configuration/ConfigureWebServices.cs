using Web.Services;
using Web.Interfaces;
namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        // register web service
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdendityViewModelService, IdendityViewModelService>();
            return services;
        }
    }
}
