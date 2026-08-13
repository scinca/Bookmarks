using Bookmarks.Features.BookmarkGroups;
namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class CreateGroupMapper(LinkGenerator linkGenerator): Mapper<Request, Response, BookmarkGroup>
{
    public override BookmarkGroup ToEntity(Request r)
        => new()
        {
            Name = r.Name,
            Description = r.Description,
            OwnerId = r.UserId!
        };

    public override Response FromEntity(BookmarkGroup e)
        => new()
        {
            Url = linkGenerator.GetPathByName(GroupEndpoints.GetGroupById,
                new RouteValueDictionary{["GroupId"] = e.Id}),
        };
}