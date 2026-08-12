namespace Bookmarks.Features.Bookmarks.Endpoints.Trash;

public class Response
{
    public int Id {get; init;}
    public string Name {get; init;}
    public string Url {get; init;}
    
    public DateTime CreatedAt {get; init;}
    public DateTime LastModifiedAt {get; init;}
}