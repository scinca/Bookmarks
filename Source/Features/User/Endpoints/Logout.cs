using Microsoft.AspNetCore.Identity;

namespace Bookmarks.Features.User.Endpoints;

public class Logout(SignInManager<ApplicationUser> signInManager): EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("auth/logout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await signInManager.SignOutAsync();
        await Send.NoContentAsync(ct);
    }
}