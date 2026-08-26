using System.Net;
using Bookmarks.Features.BookmarkGroups.Endpoints;
using Bookmarks.Features.Bookmarks.Endpoints;

namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class GetBookmarkGroupByIdTests(App App) : TestBase<App>
{
    [Fact, Priority(1)]
    public async Task InvalidGroupId_ShouldFail()
    {
        var (rsp, _) = await App.UserAClient.GETAsync<GetBookmarkByIdEndpoint, GetBookmarkGroupByIdRequest,GetBookmarkGroupByIdResponse>(new()
        {
            Id = int.MinValue,
        });
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
 
}