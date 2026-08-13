using EntityFramework.Exceptions.Common;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class Endpoint(AppDbContext context): Endpoint<Request, Response, CreateGroupMapper>
{
    public override void Configure()
    {
        Post("/group");
        Description(x => x.WithName(GroupEndpoints.CreateGroup));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = Map.ToEntity(req);
        try
        {
            context.BookmarkGroups.Add(group);
            await context.SaveChangesAsync(cancellationToken: ct);
        }
        catch (UniqueConstraintException)
        {
            AddError(x => x.Name, "Name must be unique");
            ThrowIfAnyErrors();
        }
        
        await SendMappedAsync(group, StatusCodes.Status201Created, ct);
    }
}