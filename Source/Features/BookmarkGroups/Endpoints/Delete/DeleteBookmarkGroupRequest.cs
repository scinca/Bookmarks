namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class DeleteBookmarkGroupRequest
{
    [RouteParam]
    public int Id { get; init; }
}
