using static TravelInspiration.API.Features.Stops.CreateStop;

namespace TravelInspiration.API.Shared.Domain.Entities;

public sealed class Stop(string name) : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Uri? ImageUri { get; set; }
    public int ItineraryId { get; set; }
    public Itinerary? Itinerary { get; set; }

    public void HandleCreateCommand(CreateStopCommand request)
    {
        ItineraryId = request.ItineraryId;
        ImageUri = request.ImageUri != null 
            ? new Uri(request.ImageUri) : null;
    }
}
