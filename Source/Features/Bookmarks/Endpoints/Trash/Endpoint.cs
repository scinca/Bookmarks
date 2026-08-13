using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints.Trash;

public class Endpoint(AppDbContext context) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/trash");
        Description(x => x.WithName(BookmarkEndpoints.Trash));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var trashedItems = await context.Bookmarks
                                  .AsNoTracking()
                                  .IgnoreQueryFilters([QueryFilters.ArchivedFilter, QueryFilters.SoftDeletionFilter])
                                  .Where(b => b.IsDeleted == true)
                                  .Select(b => new Response
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                      Url = b.Url,
                                      CreatedAt =  b.CreatedAt,
                                      LastModifiedAt = b.LastModifiedAt,
                                  })
                                  .ToListAsync(cancellationToken: ct);
        
        await Send.OkAsync(trashedItems, ct);
    }
}