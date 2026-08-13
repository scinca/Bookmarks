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

    public override Response FromEntity(Bookmark e)
        => new()
        {
            Url = linkGenerator.GetPathByName(
                BookmarkEndpoints.GetById,
                new() { ["Id"] = e.Id })
        };
}
