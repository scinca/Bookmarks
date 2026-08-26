using System.Net;
using Bookmarks.Features.BookmarkGroups.Endpoints;
namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class CreateTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Create_WithValidData(){
        var (rsp, res) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new ()
        {
            Name= "TestGroup",
            Description = null
        });
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
    }

    [Fact]
    public async Task Create_TwoUsersSameData()
    {
        var (rsp1, res1) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new()
        {
         Name = "SameTest",
         Description = "Same Test Description"
        });

        var (rsp2, res2) = await App.UserBClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new()
        {
                                    Name = "SameTest",
                                    Description = "Same Test Description"
        });
        
        rsp1.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res1.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
        
        rsp2.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res2.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
        
    }
}