namespace Bookmarks.Features.Bookmarks.Endpoints;

public class GetBookmarkByIdResponse
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
    
    public int? GroupId {get; set;}
}