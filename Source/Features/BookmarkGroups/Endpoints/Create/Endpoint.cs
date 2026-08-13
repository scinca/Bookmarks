using Bookmarks.Features.User.Services;
using EntityFramework.Exceptions.Common;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class Endpoint(AppDbContext context): Endpoint<Request, Response, CreateGroupMapper>
{
    public override void Configure()
    {
        Post("/group");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = Map.ToEntity(req);
        try
        {
            await context.BookmarkGroups.AddAsync(entity: group, cancellationToken: ct);
            await context.SaveChangesAsync(cancellationToken: ct);
        }
        catch (UniqueConstraintException)
        {
            AddError(x => x.Name, "Name must be unique");
        }
        finally
        {
            ThrowIfAnyErrors();
        }
        await SendMappedAsync(group, StatusCodes.Status201Created, ct);
    }
}