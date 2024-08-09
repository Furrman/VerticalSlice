using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.Features.Itineraries;

public static class GetItinerary
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries", async
            (string? searchFor,
                ILoggerFactory loggerFactory,
                IMapper mapper,
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
            var result = mapper.Map<IEnumerable<ItineraryDto>>(itineraries);

            return Results.Ok(result);
        });
    }

    public sealed class ItineraryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string UserId { get; set; }
    }

    public sealed class ItineraryProfile : Profile
    {
        public ItineraryProfile()
        {
            CreateMap<Itinerary, ItineraryDto>();
        }
    }
}
