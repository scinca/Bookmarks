
namespace Bookmarks.Common;

public class FakeCurrentUserService(string id) : ICurrentUserService
{
    public string? Id { get; } = id;
}