using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.Features.Itineraries;

public static class GetItinerary
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries", async
            (string? searchFor,
                ILoggerFactory loggerFactory,
                TravelInspirationDbContext dbContext,
                CancellationToken cancellationToken) =>
        {
            loggerFactory.CreateLogger("EndpointHandlers")
                .LogInformation("GetItineraries feature called.");

            var itineraries = await dbContext.Itineraries
                .Where(i => 
                    searchFor == null || i.Name.Contains(searchFor) ||
                    (i.Description != null && i.Description.Contains(searchFor)))
                .ToListAsync(cancellationToken);
            //var result = 

            return Results.Ok(itineraries);
        });
    }

    public sealed class ItineraryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string UserId { get; set; }
    }
}
