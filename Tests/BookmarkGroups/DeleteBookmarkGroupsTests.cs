using System.Net;
using Bookmarks;
using Bookmarks.Features.BookmarkGroups.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class DeleteBookmarkGroupsTests(App App) : TestBase<App>
{
    
    [Fact]
    public async Task Delete_WithInvalidId_ShouldFail()
    {
        var (rsp, res) = await App.UserAClient
                                  .DELETEAsync<DeleteBookmarkGroupEndpoint, DeleteBookmarkGroupRequest, EmptyResponse>(
                                      new()
                                      {
                                          Id = int.MinValue // Sqlite Primary Keys won't be negative so this key won't exist for any user.
                                      });

        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithValidData_ShouldSucceed()
    {
        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.BookmarkGroups.Add(new ()
            {
                Id = 1,
                Name = "Delete_WithValidData_Test",
                Description = null,
                CreatedAt = DateTime.UtcNow,
                OwnerId = App.UserAId!
            });
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }
        
        
        var (res, _) = await App.UserAClient
                                  .DELETEAsync<DeleteBookmarkGroupEndpoint, DeleteBookmarkGroupRequest, EmptyResponse>(
                                      new()
                                      {
                                          Id = 1
                                      });
        res.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var stillInDb = await db.BookmarkGroups.AsNoTracking().IgnoreQueryFilters().Where(bg=> bg.Id == 1).AnyAsync(TestContext.Current.CancellationToken);
            stillInDb.ShouldBeFalse();
        }
    }
    
    [Fact]
    public async Task Delete_FromOtherUser_ShouldFail()
    {
        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.BookmarkGroups.Add(new ()
            {
                Id = 10,
                Name = Guid.NewGuid().ToString(),
                Description = null,
                CreatedAt = DateTime.UtcNow,
                OwnerId = App.UserAId!
            });
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }

        var (rsp, _) = await App.UserBClient.DELETEAsync<DeleteBookmarkGroupEndpoint, DeleteBookmarkGroupRequest, EmptyResponse>(new()
        {
            Id = 10
        });
        
        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var stillInDb = await db.BookmarkGroups.AsNoTracking().IgnoreQueryFilters().Where(bg => bg.Id == 10).AnyAsync(TestContext.Current.CancellationToken);
            stillInDb.ShouldBeTrue();
        }
    }
}