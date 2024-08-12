using Microsoft.EntityFrameworkCore;
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
        await _sliceFixture.ExecuteInTransactionAsync(async (context) =>
        {
            // Arrange
            var itinerary = new Itinerary("Test", "SomeUserId");
            context.Add(itinerary);
            await context.SaveChangesAsync();

            var cmd = new CreateStop.CreateStopCommand(itinerary.Id,
                "A stop for testing",
                null
            );

            // Act
            await _sliceFixture.SendAsync(cmd);

            // Assert
            context.ChangeTracker.Clear();
            var stop = await context.Stops
                .FirstOrDefaultAsync(s => s.Name == "A stop for testing");
            Assert.NotNull(stop);
            Assert.Equal(cmd.ItineraryId, stop.ItineraryId);
            Assert.Equal(cmd.Name, stop.Name);
        });
    }
}
