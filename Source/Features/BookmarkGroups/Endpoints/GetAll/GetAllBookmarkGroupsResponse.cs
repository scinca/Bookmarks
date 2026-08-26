namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class GetAllBookmarkGroupsResponse
{
        public int Id { get; init; }
        public string Name {get; init;}
        public string? Description {get; init;}
        public DateTime CreatedAt {get; init;}
        
        public int ItemsCount {get; init;}
}