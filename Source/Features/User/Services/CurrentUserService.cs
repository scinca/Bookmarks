using System.Security.Claims;

namespace Bookmarks.Features.User.Services;

public class CurrentUserService(IHttpContextAccessor context): ICurrentUserService
{
    public string? Id => context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
