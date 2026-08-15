namespace Bookmarks.Features.BookmarkGroups.Endpoints.Delete;

public class Request
{
    [RouteParam]
    public int Id { get; init; }
}
