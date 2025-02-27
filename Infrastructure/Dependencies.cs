using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class Dependencies
{
    // These registered DbContext are registered with a Scoped lifetime.
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        //todo : add in memory database and seed data
        //bool useOnlyInMemoryDatabase = false;
        //if (configuration["UseOnlyInMemoryDatabase"] != null)
        //{
        //    useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);
        //}
        services.AddDbContext<WebContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WebConnection")));
    }
}

