using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.GetById;

public class Endpoint(AppDbContext context) : Endpoint<Request, Response>
{
    
    public override void Configure()
    {
        Get("/group/{GroupId:int}");
        Description(x =>
            x.WithName(GroupEndpoints.GetGroupById));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var group = await context
                          .BookmarkGroups
                                 .AsNoTracking()
                                 .Where(g => g.Id == req.Id)
                                 .Select(g => new Response
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