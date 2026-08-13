namespace Bookmarks.Features.Bookmarks.Endpoints.Update;

public class Endpoint(AppDbContext context) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/bookmark");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var bookmark = await context.Bookmarks.FindAsync([req.Id], cancellationToken: ct);

        if (bookmark is null)
        {
            await Send.NotFoundAsync(cancellation: ct);
            return;
        }

        if (req.Name is not null)
        {
            bookmark.Name = req.Name;
        }

        if (req.Url is not null)
        {
            bookmark.Url = req.Url;
        }

        if (req.DescriptionChanged is true)
        {
            bookmark.Description = req.Description;
        }

        if (req.IsArchived is not null)
        {
            bookmark.IsArchived = req.IsArchived.Value;
        }

        if (req.IsFavourite is not null)
        {
            bookmark.IsFavourite = req.IsFavourite.Value;
        }

        if (req.GroupChanged is true)
        {
            bookmark.GroupId = req.GroupId;
        }
        
        
        await context.SaveChangesAsync(cancellationToken: ct);
        
        await Send.NoContentAsync(cancellation: ct);
        
    }
}