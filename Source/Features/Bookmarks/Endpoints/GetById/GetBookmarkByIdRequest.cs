namespace Bookmarks.Features.Bookmarks.Endpoints;

public class GetBookmarkByIdRequest
{
    [RouteParam]
    public int Id {get; init;}
}