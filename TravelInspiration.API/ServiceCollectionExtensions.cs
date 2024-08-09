using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelInspiration.API.Shared.Behaviours;
using TravelInspiration.API.Shared.Metrics;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // HttpClients
        services.AddScoped<IDestinationSearchApiClient, DestinationSearchApiClient>();

        // Slices
        services.RegisterSlices();

        // Dependencies
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(currentAssembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(currentAssembly)
                .AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>))
                .AddOpenBehavior(typeof(HandlerPerformanceMetricBehaviour<,>));
        });
        services.AddSingleton<HandlerPerformanceMetric>();

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
