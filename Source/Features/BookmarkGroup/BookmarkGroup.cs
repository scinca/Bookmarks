using System.Text.Json.Serialization;
using Bookmarks.Features.Bookmarks;
using Bookmarks.Features.User;

namespace Bookmarks.Features.BookmarkGroup;

public class BookmarkGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public IReadOnlyCollection<Bookmark> Bookmarks { get; }
    
    [JsonIgnore]
    public ApplicationUser Owner {get; init;}
    public string OwnerId {get; init;}
}