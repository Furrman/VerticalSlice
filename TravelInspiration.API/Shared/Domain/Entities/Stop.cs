using TravelInspiration.API.Shared.Domain.Events;
using TravelInspiration.API.Shared.DomainEvents;
using static TravelInspiration.API.Features.Stops.CreateStop;

namespace TravelInspiration.API.Shared.Domain.Entities;

public sealed class Stop(string name) : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Uri? ImageUri { get; set; }
    public bool? Suggested { get; set; }
    public int ItineraryId { get; set; }
    public Itinerary? Itinerary { get; set; }
    public IList<DomainEvent> DomainEvents { get; } = [];

    public void HandleCreateCommand(CreateStopCommand request)
    {
        ItineraryId = request.ItineraryId;
        ImageUri = request.ImageUri != null 
            ? new Uri(request.ImageUri) : null;
        DomainEvents.Add(new StopCreatedEvent(this));
    }
}
