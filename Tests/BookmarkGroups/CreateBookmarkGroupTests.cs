using Bookmarks.Features.BookmarkGroups.Endpoints;
namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class CreateTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Create_WithValidData(){
        var (rsp, res) = await App.UserAClient.POSTAsync<CreateBookmarkGroup, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new ()
        {
            Name= "TestGroup",
            Description = null
        });
        
        rsp.IsSuccessStatusCode.ShouldBeTrue();
    }
}