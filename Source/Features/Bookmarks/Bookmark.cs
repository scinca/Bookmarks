using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.User;

namespace Bookmarks.Features.Bookmarks;

public class Bookmark
{
    public int Id {get; init;}
    public required string Name {get; set;}
    public  required string Url {get; set;}
    public string? Description {get; set;}
    
    public bool IsArchived {get; set;}
    public bool IsFavourite {get; set;}
    
    public bool IsDeleted {get; set;}

    public DateTime CreatedAt { get; init; }
    public DateTime LastModifiedAt {get; set;}

    public ApplicationUser Owner { get; init; } = null!;
    public required string OwnerId {get; init;}
    public BookmarkGroup? Group { get; init; }
    public int? GroupId {get; set;}
}


public static class BookmarkEndpointNames
{
    public const string CreateBookmark = nameof(CreateBookmark);
    public const string DeleteBookmark =  nameof(DeleteBookmark);
    public const string EmptyTrash =  nameof(EmptyTrash);
    public const string GetAll =  nameof(GetAll);
    public const string GetById =  nameof(GetById);
    public const string Trash = nameof(Trash);
    public const string UpdateBookmark =  nameof(UpdateBookmark);
}