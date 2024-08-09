using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class GetStops : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("api/itineraries/{itineraryId}/stops", async
            (int itineraryId,
                IMediator mediator,
                CancellationToken cancellationToken) =>
    {
            return await mediator.Send(
                new GetStopsQuery(itineraryId),
                cancellationToken);
        });
    }

    public class GetStopsQuery(int itineraryId) : IRequest<IResult>
    {
        public int ItineraryId { get; } = itineraryId;
    }

    public class GetStopsQueryHandler : IRequestHandler<GetStopsQuery, IResult>
    {
        private readonly TravelInspirationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStopsQueryHandler(TravelInspirationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(GetStopsQuery request, CancellationToken cancellationToken)
        {
            var itinerary = await _dbContext.Itineraries
                .Include(i => i.Stops)
                .FirstOrDefaultAsync(i => i.Id == request.ItineraryId, cancellationToken);

            if (itinerary == null)
            {
                return Results.NotFound();
            }
            var result = _mapper.Map<IEnumerable<StopDto>>(itinerary.Stops);

            return Results.Ok(result);
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

}