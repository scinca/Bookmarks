using Bookmarks.Features.User.Services;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.Create;

public class Endpoint(AppDbContext context, ICurrentUserService currentUser): Endpoint<Request>
{
    public override void Configure()
    {
        Post("/bookmark");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var displayName = req.Name ?? req.Url;
        
        var now = DateTime.Now;
        var bookmark = new Bookmark
        {
            Name = displayName,
            Url = req.Url,
            Description = req.Description,
            GroupId = req.GroupId,
            OwnerId = currentUser.Id!
        };

        try
        {
            await context.Bookmarks.AddAsync(bookmark, cancellationToken: ct);
            await context.SaveChangesAsync(ct);
        }
        catch (UniqueConstraintException ex)
        {
            // Sqlite does not populate constraint name or props
            AddError(e => e.Url, "Url or name already exists");

        }
        finally
        {
            ThrowIfAnyErrors();
        }

        await Send.CreatedAtAsync<GetById.Endpoint>(
            routeValues: new { id = bookmark.Id },
            cancellation: ct);


    }
}