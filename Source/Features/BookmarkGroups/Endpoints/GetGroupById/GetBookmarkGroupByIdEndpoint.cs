using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class GetBookmarkGroupByIdEndpoint(AppDbContext context) : Endpoint<GetBookmarkGroupByIdRequest, GetBookmarkGroupByIdResponse>
{
    
    public override void Configure()
    {
        Get("/group/{Id:int}");
        Description(x => x.WithName(GroupEndpoints.GetGroupById));
    }

    public override async Task HandleAsync(GetBookmarkGroupByIdRequest req, CancellationToken ct)
    {
        var group = await context
                          .BookmarkGroups
                                 .AsNoTracking()
                                 .Where(g => g.Id == req.Id)
                                 .Select(g => new GetBookmarkGroupByIdResponse
                                 {
                                     Id = g.Id,
                                     Name = g.Name,
                                     Description = g.Description,
                                     CreatedAt =  g.CreatedAt,
                                     Bookmarks = g.Bookmarks.Select(b => new BookmarkOverview
                                     {
                                         Id = b.Id,
                                         Name = b.Name,
                                         Url = b.Url,
                                         IsFavourite = b.IsFavourite
                                     }).ToList()
                                 })
                                 .FirstOrDefaultAsync(cancellationToken: ct);
        
        if (group is null)
        {
            await Send.NotFoundAsync(cancellation: ct);
        }
        else
        {
            await Send.OkAsync(response: group, cancellation: ct);
        }
    }
}