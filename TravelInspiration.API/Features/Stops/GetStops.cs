using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.Features.Stops;

public static class GetStops
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries/{itineraryId}/stops", async
            (int itineraryId,
                ILoggerFactory loggerFactory,
                IMapper mapper,
                TravelInspirationDbContext dbContext,
                CancellationToken cancellationToken) =>
    {
            loggerFactory.CreateLogger("EndpointHandlers")
                .LogInformation("GetStops feature called.");

            var itinerary = await dbContext.Itineraries
                .Include(i => i.Stops)
                .FirstOrDefaultAsync(i => i.Id == itineraryId, cancellationToken);

            if (itinerary == null)
            {
                return Results.NotFound();
            }
            var result = mapper.Map<IEnumerable<StopDto>>(itinerary.Stops);

            return Results.Ok(result);
        });
    }
}

public sealed class StopDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public required int ItineraryId { get; set; }
}

public sealed class StopProfile : Profile
{
    public StopProfile()
    {
        CreateMap<Stop, StopDto>();
    }
}
