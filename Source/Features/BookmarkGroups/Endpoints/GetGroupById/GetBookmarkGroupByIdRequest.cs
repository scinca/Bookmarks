namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class GetBookmarkGroupByIdRequest
{
    [RouteParam]
    public int Id {get; init;}
}