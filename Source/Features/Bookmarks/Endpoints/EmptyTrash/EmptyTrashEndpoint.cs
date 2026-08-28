using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints;

public class EmptyTrashEndpoint(AppDbContext context) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/trash");
        Description(x => x.WithName(BookmarkEndpointNames.EmptyTrash));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await context.Bookmarks
                     .Where(b => b.IsDeleted == true)
                     .ExecuteDeleteAsync(cancellationToken: ct);
        
        await Send.NoContentAsync(cancellation: ct);
    }
}