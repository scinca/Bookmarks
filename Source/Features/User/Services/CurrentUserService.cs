using System.Security.Claims;

namespace Bookmarks.Features.User.Services;

internal class CurrentUserService(IHttpContextAccessor context): ICurrentUserService
{
    public string? Id => context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
