namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Request
{
    [QueryParam]
    public bool? IsArchived {get; init;}
    public bool? IsFavourite {get; init;}
}