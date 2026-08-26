namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class GetBookmarkGroupByIdResponse
{
    public int Id {get; init;}
    public string Name {get; init;}
    public string? Description {get; init;}
    public DateTime CreatedAt {get; init;}
    public IReadOnlyCollection<BookmarkOverview> Bookmarks {get; init;}
}

public class BookmarkOverview
{
    public int Id {get; init;}
    public string Name {get; init;}
    public string Url {get; init;}
    public bool IsFavourite {get; init;}
}
