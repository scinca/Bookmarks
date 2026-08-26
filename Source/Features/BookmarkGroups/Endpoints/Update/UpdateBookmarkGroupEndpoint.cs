namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class UpdateBookmarkGroupEndpoint(AppDbContext context): Endpoint<UpdateBookmarkGroupRequest, UpdateBookmarkGroupResponse, UpdateBookmarkGroupMapper>
{
    public override void Configure()
    {
        Patch("/group/{Id:int}");
        Description(x => x.WithName(GroupEndpoints.UpdateGroup));
    }

    public override async Task HandleAsync(UpdateBookmarkGroupRequest req, CancellationToken ct)
    {
        var group = await context.BookmarkGroups.FindAsync([req.Id], cancellationToken: ct);
        Map.UpdateEntity(req, group!); // validator checks if Id is valid via AnyAsync
        await context.SaveChangesAsync(ct);
        
        await SendMappedAsync(group, StatusCodes.Status200OK, ct);
    }
}