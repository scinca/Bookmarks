namespace Bookmarks.Features.Bookmarks.Endpoints.Update;

public class Endpoint(AppDbContext context) : Endpoint<Request, Response, UpdateMapper>
{
    public override void Configure()
    {
        Patch("/bookmark");
        Description(x => x.WithName(BookmarkEndpoints.UpdateBookmark));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var bookmark = await context.Bookmarks.FindAsync([req.Id], cancellationToken: ct);
        
        Map.UpdateEntity(req, bookmark!);
        await context.SaveChangesAsync(cancellationToken: ct);
        
        await SendMappedAsync(bookmark, StatusCodes.Status200OK, ct);
        
    }
}