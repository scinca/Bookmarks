namespace Bookmarks.Features.Bookmarks.Endpoints.Create;

public class CreateMapper : Mapper<Request, Response, Bookmark>
{
    public override Response FromEntity(Bookmark e)
        => new ()
        {
            Url = null
        }
}

//  linkGenerator.GetPathByName(GroupEndpoints.GetGroupById,
//                new() {["GroupId"] = e.Id}),