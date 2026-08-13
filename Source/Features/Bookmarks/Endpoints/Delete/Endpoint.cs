using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.Delete;

public class Endpoint(AppDbContext context): Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/bookmark/{Id:int}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var bookmark = await context.Bookmarks
                                  .FindAsync([req.Id], cancellationToken: ct);
        
        if (bookmark is null)
        {
            await Send.NotFoundAsync(cancellation: ct);
        }
        else
        {
            bookmark.IsDeleted = true;
            
            await context.SaveChangesAsync(cancellationToken: ct);
            await Send.NoContentAsync(cancellation: ct);
        }
    }
}