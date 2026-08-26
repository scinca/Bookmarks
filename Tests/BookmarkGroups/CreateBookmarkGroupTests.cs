using System.Net;
using Bookmarks.Features.BookmarkGroups.Endpoints;
namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class CreateTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Create_WithValidData_ShouldSucceed(){
        var (rsp, res) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new ()
        {
            Name= "TestGroup",
            Description = null
        });
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
    }

    [Fact]
    public async Task Create_TwoUsersSameData_ShouldSucceed()
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
        
        // first creation successful
        rsp1.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res1.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
        //second successful
        rsp2.StatusCode.ShouldBe(HttpStatusCode.Created);
        Uri.TryCreate(res2.Url, UriKind.RelativeOrAbsolute, out _).ShouldBeTrue();
        // different result ( the ID in the url should be different
        res1.Url.ShouldNotBe(res2.Url);
    }

    [Fact]
    public async Task Create_WithSameData_ShouldFail()
    {
        var (rsp1, _) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, CreateBookmarkGroupResponse>(new ()
        {
            Name= "Duplicate Test",
            Description = null
        });
        
        rsp1.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        var (rsp2, res2) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, ProblemDetails>(new ()
        {
            Name= "Duplicate Test",
            Description = null
        });
        
        rsp2.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res2.Errors.Count().ShouldBe(1);
        res2.Errors.ShouldContain(e => e.Name.Equals(nameof(CreateBookmarkGroupRequest.Name), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task Create_WithBadData_ShouldFail()
    {
        var (rsp, res) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, ProblemDetails>(new()
        {
            Name = string.Empty,
            Description = Fake.Random.String2(1234)
        });
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBeGreaterThanOrEqualTo(2);
        res.Errors.ShouldContain(e => e.Name.Equals(nameof(CreateBookmarkGroupRequest.Name), StringComparison.OrdinalIgnoreCase));
        res.Errors.ShouldContain(e => e.Name.Equals(nameof(CreateBookmarkGroupRequest.Description), StringComparison.OrdinalIgnoreCase));
        
        var (rsp2, _) = await App.UserAClient.POSTAsync<CreateBookmarkGroupEndpoint, CreateBookmarkGroupRequest, ProblemDetails>(new()
        {
            Name = Fake.Random.String2(1234),
            Description = null
        });
        
        rsp2.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBeGreaterThanOrEqualTo(1);
        res.Errors.ShouldContain(e => e.Name.Equals(nameof(CreateBookmarkGroupRequest.Name), StringComparison.OrdinalIgnoreCase));
    }
}