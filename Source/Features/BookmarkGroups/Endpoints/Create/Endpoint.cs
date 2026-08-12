using Bookmarks.Features.User.Services;
using EntityFramework.Exceptions.Common;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class Endpoint(AppDbContext context, ICurrentUserService currentUser): Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/groups");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = new BookmarkGroup
        {
            Name = req.Name,
            Description = req.Description,
            OwnerId = currentUser.Id!
        };

        try
        {
            await context.BookmarkGroups.AddAsync(entity: group, cancellationToken: ct);
            await context.SaveChangesAsync(cancellationToken: ct);
        }
        catch (UniqueConstraintException)
        {
            AddError(x => x.Name, "The name must be unique");
        }
        finally
        {
            ThrowIfAnyErrors();
        }

        var response = new Response
        {
            Id = group.Id,
            Name = group.Name,
            Description = group.Description,
            CreatedAt = group.CreatedAt,
        };

        await Send.CreatedAtAsync<GetById.Endpoint>(
            routeValues: new { GroupId = group.Id },
            responseBody: response,
            cancellation: ct);
    }
}