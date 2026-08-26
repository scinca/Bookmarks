using EntityFramework.Exceptions.Common;
namespace Bookmarks.Features.Bookmarks.Endpoints;

public class CreateBookmarkEndpoint(AppDbContext context): Endpoint<CreateBookmarkRequest, CreateBookmarkResponse, CreateBookmarkMapper>
{
    public override void Configure()
    {
        Post("/bookmark");
        Description(x => x.WithName(BookmarkEndpoints.CreateBookmark));
    }

    public override async Task HandleAsync(CreateBookmarkRequest req, CancellationToken ct)
    {
        var bookmark = Map.ToEntity(req);

        try
        {
            await context.Bookmarks.AddAsync(bookmark, cancellationToken: ct);
            await context.SaveChangesAsync(ct);
        }
        catch (UniqueConstraintException)
        {
            // Sqlite does not populate constraint name or props
            AddError(e => e.Url, "Url or name already exists");
            ThrowIfAnyErrors();

        }

        await SendMappedAsync(bookmark, StatusCodes.Status201Created, ct);
    }
}