using Bookmarks.Common.PagedResponse;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Endpoint(AppDbContext context, LinkGenerator linkGenerator) : Endpoint<Request, PagedResponse<Response>>
{
    public override void Configure()
    {
        Get("/bookmark");
        Description(x => x.WithName(BookmarkEndpoints.GetAll));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = context.Bookmarks.AsNoTracking();
        
        if(req.IsArchived is true)
        {
         query = query.IgnoreQueryFilters([QueryFilters.ArchivedFilter])
                    .Where(c => c.IsArchived == true);
         
        }else if (req.IsFavourite is true)
        {
            query = query.Where(c => c.IsFavourite == true);
        }
        
        var totalCount = await query.CountAsync(ct);

        if (totalCount > (int) req.PageSize * req.PageNumber)
        {
            await Send.NotFoundAsync(ct);
        }

        var bookmarks = await query
                           .OrderBy(c => c.Id)
                           .Paginate(req.PageNumber, req.PageSize)
                           .Select(c => new Response()
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url,
                IsArchived = c.IsArchived,
                IsFavourite = c.IsFavourite,
                CreatedAt = c.CreatedAt,
                LastModifiedAt = c.LastModifiedAt,
            })
            .ToListAsync(cancellationToken: ct);

        var routeValues = new RouteValueDictionary
        {
            ["IsArchived"] = req.IsArchived,
            ["IsFavourite"] = req.IsFavourite,
        };

        var response =  PagedResponse<Response>.Create(linkGenerator, req.PageNumber, req.PageSize, bookmarks, totalCount, BookmarkEndpoints.GetAll, routeValues);

        await Send.OkAsync(response: response, cancellation: ct);
    }

    
    
}