namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class CreateGroupMapper(LinkGenerator linkGenerator): Mapper<Request, Response, BookmarkGroup>
{
    public override BookmarkGroup ToEntity(Request r)
        => new()
        {
            Name = r.Name,
            Description = r.Description,
            OwnerId = r.UserId!
        };

    public override Task<Response> FromEntityAsync(BookmarkGroup e, CancellationToken ct)
        => Task.FromResult(
            new Response
            {
                Url = linkGenerator.GetPathByName(
                    GroupEndpoints.GetGroupById,
                    new() { ["GroupId"] = e.Id })
            });
}