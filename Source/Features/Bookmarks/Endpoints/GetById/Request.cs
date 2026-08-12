namespace Bookmarks.Features.Bookmarks.Endpoints.GetById;

public class Request
{
    [RouteParam]
    public int Id {get; init;}
}