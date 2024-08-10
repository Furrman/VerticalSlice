using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
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
        // Suggested property is not included in the command
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
        public required string Name { get; set; }
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

    public sealed class SuggestStopCreatedEventHandler(
        ILogger<SuggestStopCreatedEventHandler> logger,
        TravelInspirationDbContext dbContext)
        : INotificationHandler<StopCreatedEvent>
    {
        private readonly ILogger<SuggestStopCreatedEventHandler> _logger = logger;
        private readonly TravelInspirationDbContext _dbContext = dbContext;

        public Task Handle(StopCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Listener {listener} to domain event {domainEvent} triggered",
                GetType().Name,
                notification.GetType().Name);

            var incomingStop = notification.Stop; 

            // TODO Do AI magic here to get the suggested stop

            var stop = new Stop($"AI-ified - {incomingStop.Name}")
            {
                ImageUri = new Uri("https://herebeimages.com/aigeneratedimage.png"),
                ItineraryId = incomingStop.ItineraryId,
                Suggested = true
            };
            _dbContext.Stops.Add(stop);

            return Task.CompletedTask;
        }
    }
}
