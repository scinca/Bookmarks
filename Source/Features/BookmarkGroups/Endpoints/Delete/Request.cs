namespace Bookmarks.Features.BookmarkGroups.Endpoints.Delete;

public class Request
{
    [RouteParam]
    public int GroupId { get; init; }
}
