using TravelInspiration.API.Shared.Networking;

namespace TravelInspiration.API.Features.SearchDestinations;

public static class SearchDestinations
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/destinations", async (string? searchFor,
            ILoggerFactory loggerFactory,
            IDestinationSearchApiClient destinationSearchApiClient) =>
        {
            loggerFactory.CreateLogger("EndpointHandlers").LogInformation("SearchDestinations featured called");

            var result = await destinationSearchApiClient
                .GetDestinationsAsync(searchFor, null);

            return Results.Ok(result);
        });
    }
}
