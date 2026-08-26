namespace Bookmarks.Features.Bookmarks.Endpoints;

public class DeleteBookmarkResponse
{
    [RouteParam]
    public int Id {get; init;}
}
