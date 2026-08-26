using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints;

public class GetBookmarkByIdEndpoint(AppDbContext context) : Endpoint<GetBookmarkByIdRequest, GetBookmarkByIdResponse>
{
    public override void Configure()
    {
        Get("/bookmark/{Id:int}");
        Description(x => x.WithName(BookmarkEndpoints.GetById));
    }

    public override async Task HandleAsync(GetBookmarkByIdRequest req, CancellationToken ct)
    {
        var bookmark = await context.Bookmarks
                                    .AsNoTracking()
                                    .IgnoreQueryFilters([QueryFilters.ArchivedFilter, QueryFilters.SoftDeletionFilter])
                                    .Where(b => b.Id == req.Id)
                                    .Select(b => new GetBookmarkByIdResponse
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Url = b.Url,
                                        Description = b.Description,
                                        IsArchived =  b.IsArchived,
                                        IsFavourite = b.IsFavourite,
                                        IsDeleted = b.IsDeleted,
                                        CreatedAt = b.CreatedAt,
                                        LastModifiedAt = b.LastModifiedAt,
                                        GroupId = b.GroupId
                                    })
                                    .FirstOrDefaultAsync(cancellationToken: ct);

        if (bookmark is null)
        {
            await Send.NotFoundAsync(cancellation: ct);
        }
        else
        {
            await Send.OkAsync(response: bookmark, cancellation: ct);
        }
    }
}