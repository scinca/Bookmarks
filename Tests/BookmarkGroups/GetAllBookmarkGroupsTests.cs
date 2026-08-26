using System.Net;
using Bookmarks;
using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.BookmarkGroups.Endpoints;

namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class GetAllBookmarkGroupsTests(App App) : TestBase<App>
{
    [Fact, Priority(1)]
    public async Task GetAll_ShouldReturnEmptyCollection()
    {
        var (rsp, res) = await App.UserAClient.GETAsync<GetAllBookmarkGroupsEndpoint ,GetAllBookmarkGroupsResponse>();
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        res.Count.ShouldBe(0);
        res.BookmarkGroups.Count.ShouldBe(0);
    }

    [Fact, Priority(2)]
    public async Task GetAll_ShouldReturnAllBookmarkGroups()
    {
        const int itemCount = 10;
        
        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var groups = App.BookmarkGroupFaker([App.UserBId]).Generate(10);
            db.BookmarkGroups.AddRange(groups);
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }
        
        var (rsp, res) = await App.UserBClient.GETAsync<GetAllBookmarkGroupsEndpoint ,GetAllBookmarkGroupsResponse>();
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        res.Count.ShouldBe(itemCount);
        
        res.BookmarkGroups.Count.ShouldBe(itemCount);
        
        res.BookmarkGroups.FirstOrDefault().ShouldBeAssignableTo<GetAllBookmarkGroupsResponseModel>();
    }

    [Fact, Priority(3)]
    public async Task GetAll_ShouldNotReturnOthers_Groups()
    {
        const string groupName = "A very unique group name no one will ever use";
        
        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var group = new BookmarkGroup
            {
                Name = groupName,
                Description = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                OwnerId = App.UserBId!,
            };
            db.BookmarkGroups.Add(group);
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        } 
        
        var (rsp, res) = await App.UserAClient.GETAsync<GetAllBookmarkGroupsEndpoint ,GetAllBookmarkGroupsResponse>();
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.BookmarkGroups.Any(bg => bg.Name.Equals(groupName)).ShouldBeFalse();
        
    }
}