using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Endpoint(AppDbContext context) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/bookmark");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var conn = context.Bookmarks.AsNoTracking();
        
        if(req.IsArchived is true)
        {
         conn = conn.IgnoreQueryFilters([QueryFilters.ArchivedFilter])
                    .Where(c => c.IsArchived == true);
         
        }else if (req.IsFavourite is true)
        {
            conn = conn.Where(c => c.IsFavourite == true);
        }

        var result = await conn.Select(c => new Response
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

        await Send.OkAsync(response: result, cancellation: ct);
    }
}