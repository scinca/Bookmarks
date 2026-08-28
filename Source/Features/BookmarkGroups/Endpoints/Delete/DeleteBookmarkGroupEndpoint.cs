using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class DeleteBookmarkGroupEndpoint(AppDbContext context): Endpoint<DeleteBookmarkGroupRequest>
{
    public override void Configure()
    {
        Delete("/group/{Id:int}");
        Description(x =>
            x.WithName(BookmarkGroupEndpointNames.DeleteGroup));
    }

    public override async Task HandleAsync(DeleteBookmarkGroupRequest req, CancellationToken ct)
    {
        var result = await context.BookmarkGroups
                                  .Where(bg => bg.Id == req.Id)
                                  .ExecuteDeleteAsync(cancellationToken: ct);

        if (result is 0)
        {
            await Send.NotFoundAsync(cancellation: ct);
        }
        else
        {
            await Send.NoContentAsync(cancellation: ct);
        }
    }
}