using Bookmarks.Features.Bookmarks;
using Bookmarks.Features.User;

namespace Bookmarks.Features.BookmarkGroups;

public class BookmarkGroup
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string? Description {get; set;}
    public DateTime CreatedAt { get; init; }
    
    public IReadOnlyCollection<Bookmark> Bookmarks { get; init; }
    
    public ApplicationUser Owner {get; init;}
    public string OwnerId {get; init;}
}