using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TravelInspiration.API.Shared.Security;

namespace TravelInspiration.API.UnitTests.Shared.Security;

public class CurrentUserServiceTests
{
    [Fact]
    public void WhenGettingUser_WithNameIdentifierClaimInContext_NameIdentifierMustBeReturned()
    {
        // Arrange
        var httpContextAccesor = new Mock<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(
            [
                new(ClaimTypes.NameIdentifier, "Kevin")
            ],
            "Test",
            ClaimTypes.NameIdentifier,
            ClaimTypes.Role);
        var contextUser = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = contextUser
        };
        httpContextAccesor.Setup(x => x.HttpContext).Returns(httpContext);
        var currentUserService = new CurrentUserService(httpContextAccesor.Object);

        // Act
        var nameIdentifier = currentUserService.UserId;

        // Assert
        Assert.Equal("Kevin", nameIdentifier);
    }
}
