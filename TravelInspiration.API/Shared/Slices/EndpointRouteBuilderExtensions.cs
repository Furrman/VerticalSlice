namespace TravelInspiration.API.Shared.Slices;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapSliceEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        foreach (var slice in endpointRouteBuilder.ServiceProvider.GetServices<ISlice>())
        {
            slice.AddEndpoint(endpointRouteBuilder);
        }

        return endpointRouteBuilder;
    }
}
