namespace Bookmarks.Features.BookmarkGroups.Endpoints.GetById;

public class Request
{
    [RouteParam]
    public int Id {get; init;}
}