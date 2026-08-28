using Bookmarks.Features.Bookmarks;
using Bookmarks.Features.User;

namespace Bookmarks.Features.BookmarkGroups;

public class BookmarkGroup
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; init; }

    public IReadOnlyCollection<Bookmark> Bookmarks { get; init; } = null!;

    public ApplicationUser Owner { get; init; } = null!;
    public required string OwnerId { get; init; } = null!;
}


public static class BookmarkGroupEndpointNames
{
    public const string GetGroupById = nameof(GetGroupById);
    public const string DeleteGroup = nameof(DeleteGroup);
    public const string GetAllGroups = nameof(GetAllGroups);
    public const string UpdateGroup = nameof(UpdateGroup);
    public const string CreateGroup =  nameof(CreateGroup);
}