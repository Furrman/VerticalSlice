using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDestinationSearchApiClient, DestinationSearchApiClient>();
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(currentAssembly);
        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TravelInspirationDbConnection");
        services.AddDbContext<TravelInspirationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}
