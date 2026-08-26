namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class CreateBookmarkGroupMapper(LinkGenerator linkGenerator): Mapper<CreateBookmarkGroupRequest, CreateBookmarkGroupResponse, BookmarkGroup>
{
    public override BookmarkGroup ToEntity(CreateBookmarkGroupRequest r)
        => new()
        {
            Name = r.Name,
            Description = r.Description,
            OwnerId = r.UserId!
        };

    public override Task<CreateBookmarkGroupResponse> FromEntityAsync(BookmarkGroup e, CancellationToken ct)
        => Task.FromResult(
            new CreateBookmarkGroupResponse
            {
                Url = linkGenerator.GetPathByName(
                    GroupEndpoints.GetGroupById,
                    new() { ["GroupId"] = e.Id })
            });
}