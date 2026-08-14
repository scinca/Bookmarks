using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Endpoint(AppDbContext context, LinkGenerator linkGenerator) : Endpoint<Request, Response>
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
                           .Select(c => new ResponseModel()
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

        var response = new Response
        {
            CurrentPage = req.PageNumber,
            PageSize = req.PageSize,

            PreviousPage = CalculatePreviousPage(req.PageNumber, req.PageSize, req.IsArchived, req.IsFavourite),
            NextPage = CalculateNextPage(req.PageNumber, req.PageSize, req.IsArchived, req.IsFavourite, totalCount),

            Bookmarks = bookmarks,
            ItemCount = totalCount,
        };

        await Send.OkAsync(response: response, cancellation: ct);
    }


    private string? CalculatePreviousPage(int currentPage, PageSize pageSize, bool? IsArchived, bool? IsFavourite)
    {
        var previousPageNumber = currentPage - 1;

        if (previousPageNumber < 1)
        {
            return null;
        }
        
        var link = linkGenerator.GetPathByName(BookmarkEndpoints.GetAll, new()
        {
            ["PageNumber"] = previousPageNumber,
            ["PageSize"] = pageSize,
            ["IsArchived"] = IsArchived,
            ["IsFavourite"] = IsFavourite,
        });
        return link;
    }

    private string? CalculateNextPage(int currentPage, PageSize pageSize, bool? IsArchived, bool? IsFavourite, int totalCount)
    {
        var nextPageNumber =  currentPage + 1;
        if (totalCount <= (int)pageSize * nextPageNumber )
        {
            return null;
        }

        var link = linkGenerator.GetPathByName(BookmarkEndpoints.GetAll, new()
        {
            ["PageNumber"] = nextPageNumber,
            ["PageSize"] = pageSize,
            ["IsArchived"] = IsArchived,
            ["IsFavourite"] = IsFavourite,     
        });

        return link;
    }
    
}