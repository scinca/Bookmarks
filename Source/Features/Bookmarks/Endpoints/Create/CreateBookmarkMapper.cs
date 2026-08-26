namespace Bookmarks.Features.Bookmarks.Endpoints;

public class CreateBookmarkMapper(LinkGenerator linkGenerator) : Mapper<CreateBookmarkRequest, CreateBookmarkResponse, Bookmark>
{
    public override Bookmark ToEntity(CreateBookmarkRequest r)
        => new ()
        {
            Name = r.Name ?? r.Url,
            Url = r.Url,
            Description = r.Description,
            GroupId = r.GroupId,
            OwnerId = r.UserId
        };

    public override Task<CreateBookmarkResponse> FromEntityAsync(Bookmark e, CancellationToken ct)
        => Task.FromResult(
            new CreateBookmarkResponse
            {
                Url = linkGenerator.GetPathByName(
                    BookmarkEndpoints.GetById,
                    new() { ["Id"] = e.Id })
            });
}
