using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.GetAll;

public class Endpoint(AppDbContext context) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/group");
        Description(x =>
            x.WithName(GroupEndpoints.GetAll));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groups = await context
                         .BookmarkGroups
                         .AsNoTracking()
                         .Select(bg => new Response
                         {
                             Id = bg.Id,
                             Name = bg.Name,
                             Description = bg.Description,
                             CreatedAt = bg.CreatedAt,
                             ItemsCount = bg.Bookmarks.Count
                         })
                         .ToListAsync(cancellationToken: ct);

        if (groups.Count == 0)
        {
            await Send.NoContentAsync(cancellation: ct);
        }
        else
        {
            await Send.OkAsync(groups, ct);
        }
    }
    
}