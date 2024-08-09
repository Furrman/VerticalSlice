using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class CreateStop : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder) =>
        endpointRouteBuilder.MapPost("/api/itineraries/{itineraryId}/stops",
            async (int itineraryId,
                CreateStopCommand createStopCommand,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                createStopCommand.ItineraryId = itineraryId;
                return await mediator.Send(createStopCommand, cancellationToken);
            });

    public sealed class CreateStopCommand(int itineraryId,
        string Name,
        string? imageUri) : IRequest<IResult>
    {
        public int ItineraryId { get; set; } = itineraryId;
        public string Name { get; set; } = Name;
        public string? ImageUri { get; set; } = imageUri;
    }

    public class CreateStopRequestHandler(
        IMapper mapper,
        TravelInspirationDbContext dbContext)
        : IRequestHandler<CreateStopCommand, IResult>
    {
        private readonly TravelInspirationDbContext _dbContext = dbContext;

        public async Task<IResult> Handle(CreateStopCommand request, 
            CancellationToken cancellationToken)
        {
            var itinerary = await _dbContext.Itineraries
                .Include(i => i.Stops)
                .FirstOrDefaultAsync(i => 
                    i.Id == request.ItineraryId, 
                    cancellationToken);

            if (itinerary == null)
            {
                return Results.NotFound();
            }

            var stop = new Stop(request.Name);
            stop.HandleCreateCommand(request);

            _dbContext.Stops.Add(stop);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<StopDto>(stop); 

            return Results.Created(
                $"api/itineraries/{stop.ItineraryId}/stops/{stop.Id}",
                result);
        }
    }

    public sealed class StopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri? ImageUri { get; set; }
        public int ItineraryId { get; set; }
    }

    public sealed class StopMapProfileAfterCreation : Profile
    {
        public StopMapProfileAfterCreation()
        {
            CreateMap<Stop, StopDto>();
        }
    }
}
