using Bookmarks.Features.BookmarkGroups.Endpoints.Create;
namespace Tests.BookmarkGroups.Create;

public class CreateTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Create_WithValidData(){
        var (rsp, res) = await App.UserAClient.POSTAsync<Endpoint, Request, Response>(new ()
        {
            Name= "TestGroup",
            Description = null
        });
        
        rsp.IsSuccessStatusCode.ShouldBeTrue();
    }
}