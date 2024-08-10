using AutoMapper;
using FluentValidation;
using MediatR;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class UpdateStop : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder) =>
        endpointRouteBuilder.MapPut("/api/itineraries/{itineraryId}/stops/{stopId}",
            async (int itineraryId,
                    int stopId,
                    UpdateStopCommand updateStopCommand,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
            {
                updateStopCommand.ItineraryId = itineraryId;
                updateStopCommand.StopId = stopId;
                return await mediator.Send(updateStopCommand, cancellationToken);
            });

    public sealed class  UpdateStopCommand : IRequest<IResult>
    {
        public int ItineraryId { get; set; }
        public int StopId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUri { get; set; }
        public bool? Suggested { get; set; }
    }

    public sealed class UpdateStopCommandValidator : AbstractValidator<UpdateStopCommand>
    {
        public UpdateStopCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.ImageUri)
                .MaximumLength(2000)
                .Must(ImageUri => Uri.TryCreate(ImageUri ?? "", UriKind.Absolute, out _))
                .When(v => !string.IsNullOrEmpty(v.ImageUri))
                .WithMessage("ImageUri must be valid Uri.");
        }   
    }

    public sealed class UpdateStopRequestHandler(
        IMapper mapper,
        TravelInspirationDbContext dbContext) 
        : IRequestHandler<UpdateStopCommand, IResult>
    {
        private readonly IMapper _mapper = mapper;
        private readonly TravelInspirationDbContext _dbContext = dbContext;

        public async Task<IResult> Handle(UpdateStopCommand request, 
            CancellationToken cancellationToken)
        {
            var stop = await _dbContext.Stops.FindAsync(request.StopId,
                cancellationToken);

            if (stop == null)
            {
                return Results.NotFound();
            }

            stop.Name = request.Name;
            stop.ImageUri = request.ImageUri != null
                ? new Uri(request.ImageUri) : null;
            stop.Suggested = request.Suggested;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(_mapper.Map<StopDto>(stop));
        }
    }

    public sealed class StopDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Uri? ImageUri { get; set; }
        public int ItineraryId { get; set; }
        public bool? Suggested { get; set; }
    }

    public sealed class UpdateStopProfile : Profile
    {
        public UpdateStopProfile()
        {
            CreateMap<Stop, StopDto>();
        }
    }
}
