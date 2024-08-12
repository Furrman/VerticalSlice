using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
using static TravelInspiration.API.Features.Stops.CreateStop;

namespace TravelInspiration.API.UnitTests.Shared.Domain;

public class StopTests : IDisposable
{
    private readonly Stop _stop;
    private readonly CreateStopCommand _createStopCommand;

    public StopTests()
    {
        _stop = new Stop("StopForTesting");
        _createStopCommand = new CreateStopCommand(42,
            "A name",
            null);
    }

    // unitofwork_stateundertest_expectedbehavior
    // given_when_then

    [Fact]
    public void WhenExecutingHandleCreateCommand_WithItineraryId_StopItineraryIdMustMatch()
    {
        // Arrange

        // Act
        _stop.HandleCreateCommand(_createStopCommand);

        // Assert
        Assert.Equal(_createStopCommand.ItineraryId, _stop.ItineraryId);
    }

    [Fact]
    public void WhenExecutingHandleCreateCommand_WithValidInput_OneStopCreatedEventMustBeAdded()
    {
        // Arrange
        var stop = new Stop("StopForTesting");
        var createStopCommand = new CreateStopCommand(42,
            "A name",
            null);

        // Act
        stop.HandleCreateCommand(createStopCommand);

        // Assert
        Assert.Single(stop.DomainEvents);
        Assert.IsType<StopCreatedEvent>(stop.DomainEvents[0]);
    }

    public void Dispose()
    {
        // No code in here
    }
}