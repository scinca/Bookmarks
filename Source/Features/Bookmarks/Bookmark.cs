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


public static class BookmarkEndpoints
{
    public const string CreateBookmark = "CreateBookmark";
    public const string DeleteBookmark =  "DeleteBookmark";
    public const string EmptyTrash =  "EmptyTrash";
    public const string GetAll =  "GetAll";
    public const string GetById =  "GetById";
    public const string Trash = "Trash";
    public const string UpdateBookmark =  "UpdateBookmark";
}