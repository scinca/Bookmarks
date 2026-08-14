namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Response
{
    public int CurrentPage {get; init;}
    public PageSize PageSize {get; init;}
    
    
    //these will be links for hateoas style
    public string? PreviousPage {get; init;}
    public string? NextPage {get; init;}
    
    
    public IReadOnlyCollection<ResponseModel> Bookmarks {get; init;}
    public int ItemCount {get; init;}
    
}


public class ResponseModel
{
    public int Id {get; init;}
    public string Name {get; init;}
    public string Url {get; init;}
    public bool IsArchived {get; init;}
    public bool IsFavourite {get; init;}
    public DateTime CreatedAt {get; init;}
    public DateTime LastModifiedAt {get; init;}
}