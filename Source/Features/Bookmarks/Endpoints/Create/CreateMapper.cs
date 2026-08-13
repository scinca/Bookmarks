namespace Bookmarks.Features.Bookmarks.Endpoints.Create;

public class CreateMapper(LinkGenerator linkGenerator) : Mapper<Request, Response, Bookmark>
{
    public override Response FromEntity(Bookmark e)
        => new()
        {
            Url = linkGenerator.GetPathByName(
                BookmarkEndpoints.GetById,
                new() { ["Id"] = e.Id })
        };
}
