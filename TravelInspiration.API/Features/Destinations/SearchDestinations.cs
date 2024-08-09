using TravelInspiration.API.Shared.Networking;

namespace TravelInspiration.API.Features.Destinations;

public static class SearchDestinations
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/destinations", 
            async (string? searchFor,
                ILoggerFactory loggerFactory,
                IDestinationSearchApiClient destinationSearchApiClient,
                CancellationToken cancellationToken) =>
        {
            loggerFactory.CreateLogger("EndpointHandlers")
                .LogInformation("SearchDestinations featured called");

            var resultFromApiCall = await destinationSearchApiClient
                .GetDestinationsAsync(searchFor, cancellationToken);

            var result = resultFromApiCall.Select(x => new
            {
                x.Name,
                x.Description,
                x.ImageUri
            });

            return Results.Ok(result);
        });
    }
}
