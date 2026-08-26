namespace Bookmarks.Features.Bookmarks.Endpoints;

public class UpdateBookmarkMapper(LinkGenerator linkGenerator) : Mapper<UpdateBookmarkRequest, UpdateBookmarkResponse, Bookmark>
{
    public override Bookmark UpdateEntity(UpdateBookmarkRequest r, Bookmark e)
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
        
        e.LastModifiedAt = DateTime.Now;
        
        return e;
    }
    
    
    public override Task<UpdateBookmarkResponse> FromEntityAsync(Bookmark e, CancellationToken ct)
        => Task.FromResult(new UpdateBookmarkResponse
        {
            Url = linkGenerator.GetPathByName(
                BookmarkEndpoints.GetById,
                new() { ["Id"] = e.Id })
        });
}