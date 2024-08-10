using MediatR;
using TravelInspiration.API.Shared.Domain.Events;

namespace TravelInspiration.API.Shared.DomainHandlers;

public sealed class SuggestStopStopCreatedEventHandler(
    ILogger<SuggestStopStopCreatedEventHandler> logger)
    : INotificationHandler<StopCreatedEvent>
{
    private readonly ILogger<SuggestStopStopCreatedEventHandler> _logger = logger;

    public Task Handle(StopCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listener {listener} to domain event {domainEvent} triggered",
            GetType().Name,
            notification.GetType().Name);

        // TODO Do AI magic here to get the suggested stop

        return Task.CompletedTask;
    }
}
