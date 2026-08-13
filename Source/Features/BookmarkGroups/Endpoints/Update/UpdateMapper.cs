namespace Bookmarks.Features.BookmarkGroups.Endpoints.Update;

public class UpdateMapper : Mapper<Request, Response, BookmarkGroup>
{
    public override BookmarkGroup UpdateEntity(Request r, BookmarkGroup e)
    {
        if (r.Name is not null)
        {
            e.Name = r.Name;
        }

        if (r.ChangedDescription is true)
        {
            e.Description = r.Description;
        }
        return e;
    }


    public override Task<Response> FromEntityAsync(BookmarkGroup e, CancellationToken ct)
        => Task.FromResult(
            new Response
            {
                GroupId = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
            });
}