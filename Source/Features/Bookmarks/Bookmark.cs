using System.Text.Json.Serialization;
using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.User;

namespace Bookmarks.Features.Bookmarks;

public class Bookmark
{
    public int Id {get; init;}
    public string Name {get; set;}
    public string Url {get; set;}
    public string? Description {get; set;}
    
    public bool IsArchived {get; set;}
    public bool IsFavourite {get; set;}
    
    public bool IsDeleted {get; set;}
    
    public DateTime CreatedAt {get; init;}
    public DateTime LastModifiedAt {get; set;}
    
    
    [JsonIgnore]
    public ApplicationUser Owner {get; init;}
    public string OwnerId {get; init;}
    
    [JsonIgnore]
    public BookmarkGroup? Group { get; set; }
    public int? GroupId {get; set;}
}