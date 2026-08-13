using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.EmptyTrash;

public class Endpoint(AppDbContext context) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/trash");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await context.Bookmarks
                     .Where(b => b.IsDeleted == true)
                     .ExecuteDeleteAsync(cancellationToken: ct);
        
        await Send.NoContentAsync(cancellation: ct);
    }
}