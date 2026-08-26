namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class UpdateBookmarkGroupResponse
{
    public int GroupId {get; init;}
    public string Name {get; init;}
    public string? Description {get; init;}
    public DateTime CreatedAt {get; init;}
}