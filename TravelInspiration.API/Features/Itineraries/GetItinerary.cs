using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Itineraries;

public sealed class GetItinerary : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("api/itineraries", async
            (string? searchFor,
                IMediator mediator,
                CancellationToken cancellationToken) =>
        {
            return await mediator.Send(
                new GetItineraryQuery(searchFor),
                cancellationToken);
        });
    }

    public class GetItineraryQuery(string? searchFor) : IRequest<IResult>
    {
        public string? SearchFor { get; } = searchFor;
    }

    public class GetItineraryQueryHandler : IRequestHandler<GetItineraryQuery, IResult>
    {
        private readonly TravelInspirationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetItineraryQueryHandler(TravelInspirationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(GetItineraryQuery request, CancellationToken cancellationToken)
        {
            var itineraries = await _dbContext.Itineraries
                .Where(i => request.SearchFor == null || i.Name.Contains(request.SearchFor) ||
                    (i.Description != null && i.Description.Contains(request.SearchFor)))
                .ToListAsync(cancellationToken);
            return Results.Ok(_mapper.Map<IEnumerable<ItineraryDto>>(itineraries));
        }
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
