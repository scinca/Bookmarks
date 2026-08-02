namespace Bookmarks.Features.User.Services;

public class FakeCurrentUserService(string id) : ICurrentUserService
{
    public string? Id { get; } = id;
}