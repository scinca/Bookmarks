namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class Response
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}