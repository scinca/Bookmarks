namespace Bookmarks.Features.BookmarkGroups.Endpoints.Update;

public class Endpoint(AppDbContext context): Endpoint<Request, Response, UpdateMapper>
{
    public override void Configure()
    {
        Patch("/group/{Id:int}");
        Description(x => x.WithName(GroupEndpoints.UpdateGroup));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = await context.BookmarkGroups.FindAsync([req.Id], cancellationToken: ct);
        Map.UpdateEntity(req, group!); // validator checks if Id is valid via AnyAsync
        await context.SaveChangesAsync(ct);
        
        await SendMappedAsync(group, StatusCodes.Status200OK, ct);
    }
}