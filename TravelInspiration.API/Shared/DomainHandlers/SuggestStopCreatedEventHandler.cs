using MediatR;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.Shared.DomainHandlers;

public sealed class SuggestStopCreatedEventHandler(
    ILogger<SuggestStopCreatedEventHandler> logger,
    TravelInspirationDbContext dbContext)
    : INotificationHandler<StopCreatedEvent>
{
    private readonly ILogger<SuggestStopCreatedEventHandler> _logger = logger;
    private readonly TravelInspirationDbContext _dbContext = dbContext;

    public Task Handle(StopCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listener {listener} to domain event {domainEvent} triggered",
            GetType().Name,
            notification.GetType().Name);

        // TODO Do AI magic here to get the suggested stop

        var incomingStop = notification.Stop;
        var stop = new Stop($"AI-ified - {incomingStop.Name}")
        {
            ImageUri = new Uri("https://herebeimages.com/aigeneratedimage.png"),
            ItineraryId = incomingStop.ItineraryId,
            Suggested = true
        };
        _dbContext.Stops.Add(stop);

        return Task.CompletedTask;
    }
}
