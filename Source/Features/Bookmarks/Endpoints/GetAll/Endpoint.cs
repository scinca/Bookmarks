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

        query = req.ResultFilter switch
        {
            ResultFilter.Archived => query.IgnoreQueryFilters([QueryFilters.ArchivedFilter])
                                          .Where(c => c.IsArchived == true),
            ResultFilter.Deleted => query
                                    .IgnoreQueryFilters([QueryFilters.SoftDeletionFilter])
                                    .Where(c => c.IsDeleted == true),
            ResultFilter.Favourites => query.Where(c => c.IsFavourite == true),

            _ => query // Returns All (no filters like ResulFilter.All
        };

        var totalCount = await query.CountAsync(ct);


        if (totalCount > 0 && req.PageNumber > Math.Ceiling((double) totalCount / (int) req.PageSize))
        {
            await Send.NotFoundAsync(ct);

            return;
        }

        var bookmarks = await query
                              .OrderBy(c => c.Id)
                              .Paginate(req.PageNumber, req.PageSize)
                              .Select(c => new Response()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  Url = c.Url,
                                  IsDeleted = c.IsDeleted,
                                  IsArchived = c.IsArchived,
                                  IsFavourite = c.IsFavourite,
                                  CreatedAt = c.CreatedAt,
                                  LastModifiedAt = c.LastModifiedAt,
                              })
                              .ToListAsync(cancellationToken: ct);

        var routeValues = new RouteValueDictionary
        {
            ["ResultFilter"] = req.ResultFilter,
        };

        var response = PagedResponse<Response>.Create(
            linkGenerator,
            req.PageNumber,
            req.PageSize,
            bookmarks,
            totalCount,
            BookmarkEndpoints.GetAll,
            routeValues);

        await Send.OkAsync(response: response, cancellation: ct);
    }
}