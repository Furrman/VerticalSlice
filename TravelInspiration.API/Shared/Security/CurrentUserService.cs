using System.Security.Claims;

namespace TravelInspiration.API.Shared.Security;

public interface ICurrentUserService
{
    string? UserId { get; }
}

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? UserId
    {
        get
        {
            return _httpContextAccessor.HttpContext?
                .User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}