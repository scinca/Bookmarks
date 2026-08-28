namespace Bookmarks.Features.Bookmarks.Endpoints;

public class DeleteBookmarkEndpoint(AppDbContext context): Endpoint<DeleteBookmarkResponse>
{
    public override void Configure()
    {
        Delete("/bookmark/{Id:int}");
        Description(x => x.WithName(BookmarkEndpointNames.DeleteBookmark));
    }

    public override async Task HandleAsync(DeleteBookmarkResponse req, CancellationToken ct)
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