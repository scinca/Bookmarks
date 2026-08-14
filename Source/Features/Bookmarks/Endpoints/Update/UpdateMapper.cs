namespace Bookmarks.Features.Bookmarks.Endpoints.Update;

public class UpdateMapper(LinkGenerator linkGenerator) : Mapper<Request, Response, Bookmark>
{
    public override Bookmark UpdateEntity(Request r, Bookmark e)
    {
        if (r.Name is not null)
        {
            e.Name = r.Name;
        }

        if (r.Url is not null)
        {
            e.Url = r.Url;
        }

        if (r.DescriptionChanged is true)
        {
            e.Description = r.Description;
        }

        if (r.IsArchived is not null)
        {
            e.IsArchived = r.IsArchived.Value;
        }

        if (r.IsFavourite is not null)
        {
            e.IsFavourite = r.IsFavourite.Value;
        }

        if (r.GroupChanged is true)
        {
            e.GroupId = r.GroupId;
        }
        
        return e;
    }
    
    
    public override Task<Response> FromEntityAsync(Bookmark e, CancellationToken ct)
        => Task.FromResult(new Response
        {
            Url = linkGenerator.GetPathByName(
                BookmarkEndpoints.GetById,
                new() { ["Id"] = e.Id })
        });
}