using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class CreateStop : ISlice
{
    // "scope" : "write" (or "scope" : ["read", "write"] ...)

    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder) =>
        endpointRouteBuilder.MapPost("/api/itineraries/{itineraryId}/stops",
            async (int itineraryId,
                CreateStopCommand createStopCommand,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                createStopCommand.ItineraryId = itineraryId;
                return await mediator.Send(createStopCommand, cancellationToken);
            }).RequireAuthorization(Shared.Security.AuthorizationPolicies.HasWriteActionPolicy);

    public sealed class CreateStopCommand(int itineraryId,
        string Name,
        string? imageUri) : IRequest<IResult>
    {
        public int ItineraryId { get; set; } = itineraryId;
        public string Name { get; set; } = Name;
        public string? ImageUri { get; set; } = imageUri;
        // Suggested property is not included in the command
        // because it is set by default in entity configuration
    }

    public sealed class CreateStopCommandValidator : AbstractValidator<CreateStopCommand>
    {
        public CreateStopCommandValidator()
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

    public sealed class CreateStopRequestHandler(
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
        public required string Name { get; set; }
        public Uri? ImageUri { get; set; }
        public int ItineraryId { get; set; }
        public bool? Suggested { get; set; }
    }

    public sealed class StopMapProfileAfterCreation : Profile
    {
        public StopMapProfileAfterCreation()
        {
            CreateMap<Stop, StopDto>();
        }
    }
}