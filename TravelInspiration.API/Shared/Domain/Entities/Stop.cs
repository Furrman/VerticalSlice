namespace TravelInspiration.API.Shared.Domain.Entities;

public sealed class Stop : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public int ItineraryId { get; set; }
    public Itinerary? Itinerary { get; set; }
}
