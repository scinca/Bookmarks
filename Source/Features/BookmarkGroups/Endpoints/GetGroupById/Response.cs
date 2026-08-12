namespace Bookmarks.Features.BookmarkGroups.Endpoints.GetById;

public class Response
{
    public int Id;
    public string Name;
    public string? Description;
    public DateTime CreatedAt;
    public IReadOnlyCollection<BookmarkOverview> Bookmarks;
}

public class BookmarkOverview
{
    public int Id;
    public string Name;
    public string Url;
    public bool IsFavourite;
}
