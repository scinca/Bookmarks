using System.Text.Json.Serialization;
using Bookmarks.Features.BookmarkGroups;

namespace Bookmarks.Features.Bookmarks;

public class Bookmark
{
    public int Id {get; init;}
    
    [JsonIgnore]
    public BookmarkGroup Group { get; set; }
    public int GroudId {get; set;}
}