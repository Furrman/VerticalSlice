using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelInspiration.API.Features.Stops;
using TravelInspiration.API.IntegrationTests.Fixtures;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.IntegrationTests.Features.Stops;

public class CreateStopTests(SliceFixture sliceFixture)
    : IClassFixture<SliceFixture>
{
    private readonly SliceFixture _sliceFixture = sliceFixture;

    [Fact]
    public async Task WhenExecutingCreateStopSlice_WithValidInput_StopMustBeCreated()
    {
        // Arrange
        using var scope = _sliceFixture.ServiceScopeFactory.CreateScope();
        var context = _sliceFixture.CreateContext(scope);
        await context.Database.BeginTransactionAsync();

        var itinerary = new Itinerary("Test", "SomeUserId");
        context.Add(itinerary);
        await context.SaveChangesAsync();

        var cmd = new CreateStop.CreateStopCommand(itinerary.Id,
            "A stop for testing",
            null
        );
        var mediator = scope.ServiceProvider
            .GetRequiredService<IMediator>();

        // Act
        await mediator.Send(cmd);

        // Assert
        context.ChangeTracker.Clear();
        var stop = await context.Stops
            .FirstOrDefaultAsync(s => s.Name == "A stop for testing");
        Assert.NotNull(stop);
        Assert.Equal(cmd.ItineraryId, stop.ItineraryId);
        Assert.Equal(cmd.Name, stop.Name);

        await context.Database.RollbackTransactionAsync();
    }
}
