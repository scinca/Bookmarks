namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class GetAllBookmarkGroupsResponseModel
{
        public int Id { get; init; }
        public string Name {get; init;}
        public string? Description {get; init;}
        public DateTime CreatedAt {get; init;}
        
        public int ItemsCount {get; init;}
}

public class GetAllBookmarkGroupsResponse
{
    public int Count {get; init;}
    public IReadOnlyCollection<GetAllBookmarkGroupsResponseModel> BookmarkGroups {get; init;}
    
}