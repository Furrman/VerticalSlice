using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Destinations;

public sealed class SearchDestinations : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("api/destinations", 
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
