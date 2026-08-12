namespace Bookmarks.Features.BookmarkGroups.Endpoints.Update;

public class Endpoint(AppDbContext context): Endpoint<Request, Response>
{
    public override void Configure()
    {
        Patch("/group/{GroupId:int}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = await context.BookmarkGroups.FindAsync([req.GroupId], cancellationToken: ct);

        if (group is null)
        {
            await Send.NotFoundAsync(cancellation: ct);
            return;
        }

        if (req.Name is not null)
        {
            group.Name = req.Name;
        }

        if (req.ChangedDescription is true)
        {
            group.Description = req.Description;
        }
        await context.SaveChangesAsync(ct);

        var responseDto = new Response
        {
            GroupId = group.Id,
            Name = group.Name,
            Description = group.Description,
            CreatedAt = group.CreatedAt,
        };
        await Send.OkAsync(response: responseDto, cancellation: ct);
    }
}