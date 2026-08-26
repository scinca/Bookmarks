namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class UpdateBookmarkGroupMapper : Mapper<UpdateBookmarkGroupRequest, UpdateBookmarkGroupResponse, BookmarkGroup>
{
    public override BookmarkGroup UpdateEntity(UpdateBookmarkGroupRequest r, BookmarkGroup e)
    {
        if (r.Name is not null)
        {
            e.Name = r.Name;
        }

        if (r.ChangedDescription is true)
        {
            e.Description = r.Description;
        }
        return e;
    }


    public override Task<UpdateBookmarkGroupResponse> FromEntityAsync(BookmarkGroup e, CancellationToken ct)
        => Task.FromResult(
            new UpdateBookmarkGroupResponse
            {
                GroupId = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
            });
}