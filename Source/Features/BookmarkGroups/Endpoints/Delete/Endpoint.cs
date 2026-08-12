using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Delete;

public class Endpoint(AppDbContext context): Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/group/{GroupId:int}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await context.BookmarkGroups
                                  .Where(bg => bg.Id == req.GroupId)
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