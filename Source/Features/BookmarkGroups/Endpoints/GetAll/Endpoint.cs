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
        
            await Send.OkAsync(groups, ct);
    }
}