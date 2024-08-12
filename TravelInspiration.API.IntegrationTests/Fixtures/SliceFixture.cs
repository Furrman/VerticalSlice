using Microsoft.Extensions.DependencyInjection;
using TravelInspiration.API.IntegrationTests.Factories;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.IntegrationTests.Fixtures;

public class SliceFixture
{
    public IServiceScopeFactory ServiceScopeFactory { get; }
    private readonly TravelInspirationWebApplicationFactory _factory;

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public SliceFixture()
    {
        _factory = new TravelInspirationWebApplicationFactory();
        ServiceScopeFactory = _factory.Services
            .GetRequiredService<IServiceScopeFactory>();

        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var scope = ServiceScopeFactory.CreateScope();
                using var context = CreateContext(scope);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Stops.RemoveRange(context.Stops.ToList());
                context.Itineraries.RemoveRange(context.Itineraries.ToList());
                _databaseInitialized = true;
            }
        }
    }

    public TravelInspirationDbContext CreateContext(IServiceScope scope)
    {
        var context = scope.ServiceProvider
            .GetRequiredService<TravelInspirationDbContext>();
        return context;
    }
}
