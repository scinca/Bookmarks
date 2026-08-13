namespace Bookmarks.Features.Bookmarks.Endpoints.Delete;

public class Request
{
    [RouteParam]
    public int Id {get; init;}
}
