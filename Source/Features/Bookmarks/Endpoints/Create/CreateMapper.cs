namespace Bookmarks.Features.Bookmarks.Endpoints.Create;

public class CreateMapper(LinkGenerator linkGenerator) : Mapper<Request, Response, Bookmark>
{
    public override Bookmark ToEntity(Request r)
        => new ()
        {
            Name = r.Name ?? r.Url,
            Url = r.Url,
            Description = r.Description,
            GroupId = r.GroupId,
            OwnerId = r.UserId
        };

    public override Task<Response> FromEntityAsync(Bookmark e, CancellationToken ct)
        => Task.FromResult(
            new Response
            {
                Url = linkGenerator.GetPathByName(
                    BookmarkEndpoints.GetById,
                    new() { ["Id"] = e.Id })
            });
}
