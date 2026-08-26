namespace Bookmarks.Common.PagedResponse;

public class PagedResponse<TResponse>
{
    public int CurrentPage {get; private set;}
    public PageSize PageSize { get; private set; }
    
    
    public string? PreviousPage {get; private set;}
    public string? NextPage {get; private set;}

    
    public IReadOnlyCollection<TResponse> Contents {get; private set;}
    public int ItemCount {get; private set;}
    
    
    public static PagedResponse<TResponse> Create(LinkGenerator linkGenerator,int currentPage,
                                                  PageSize pageSize,
                                                  IReadOnlyCollection<TResponse> contents,
                                                  int totalCount,
                                                  string pathName,
                                                  RouteValueDictionary? additionalRouteValues)
        => new()
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            PreviousPage = CalculatePreviousPage(linkGenerator, pathName, currentPage, pageSize, additionalRouteValues),
            NextPage = CalculateNextPage(
                linkGenerator,
                pathName,
                currentPage,
                pageSize,
                totalCount,
                additionalRouteValues),
            Contents = contents,
            ItemCount = totalCount
        };

    private static string? CalculatePreviousPage(LinkGenerator linkGenerator, string pathName,
                                                 int currentPage, PageSize pageSize, RouteValueDictionary? additionalRouteValues)
    {
        var previousPageNumber = currentPage - 1;

        if (previousPageNumber < 1)
        {
            return null;
        }
        
        var routeValues = new RouteValueDictionary
        {
            ["PageNumber"] = previousPageNumber,
            ["PageSize"] = pageSize
        };

        if(additionalRouteValues is not null)
        {
            foreach (var (key, value) in additionalRouteValues)
            {
                routeValues[key] = value;
            }
        }
        
        var link = linkGenerator.GetPathByName(pathName, routeValues);
        return link;
    }

    private static string? CalculateNextPage(LinkGenerator linkGenerator,string pathName ,int currentPage, PageSize pageSize, int totalCount,RouteValueDictionary? additionalRouteValues )
    {
        var nextPageNumber =  currentPage + 1;
        if (totalCount <= (int)pageSize * nextPageNumber )
        {
            return null;
        }

        var routeValues = new RouteValueDictionary
        {
            ["PageNumber"] = nextPageNumber,
            ["PageSize"] = pageSize
        };

        if(additionalRouteValues is not null)
        {
            foreach (var (key, value) in additionalRouteValues)
            {
                routeValues[key] = value;
            }
        }
        
        var link = linkGenerator.GetPathByName(pathName, routeValues);
        return link;
    }
}



