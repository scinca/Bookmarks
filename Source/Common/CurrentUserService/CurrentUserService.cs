using System.Security.Claims;

namespace Bookmarks.Common;

internal class CurrentUserService(IHttpContextAccessor context): ICurrentUserService
{
    public string? Id => context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
