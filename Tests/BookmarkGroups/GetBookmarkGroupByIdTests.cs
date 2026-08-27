using System.Net;
using Bookmarks;
using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.BookmarkGroups.Endpoints;
using Bookmarks.Features.Bookmarks.Endpoints;

namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class GetBookmarkGroupByIdTests(App App) : TestBase<App>
{
    [Fact, Priority(1)]
    public async Task InvalidGroupId_ShouldFail()
    {
        var (rsp, _) = await App.UserAClient
                                .GETAsync<GetBookmarkByIdEndpoint, GetBookmarkGroupByIdRequest,
                                    GetBookmarkGroupByIdResponse>(
                                    new()
                                    {
                                        Id = int.MinValue,
                                    });

        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact, Priority(2)]
    public async Task ValidGroupId_ShouldSucceed()
    {
        const int id = 2000;


        var testGroup = new BookmarkGroup
        {
            Id = id,
            Name = Guid.CreateVersion7().ToString(),
            Description = null,
            CreatedAt = DateTime.UnixEpoch,
            OwnerId = App.UserAId!,
        };

        using (var scope = App.Services.CreateScope())
        {
            var db = App.Services.GetRequiredService<AppDbContext>();
            db.BookmarkGroups.Add(testGroup);
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }

        var (rsp, res) = await App.UserAClient
                                  .GETAsync<GetBookmarkGroupByIdEndpoint, GetBookmarkGroupByIdRequest,
                                      GetBookmarkGroupByIdResponse>(
                                      new()
                                      {
                                          Id = id,
                                      });

        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.Id.ShouldBe(id);
        res.Name.ShouldBe(testGroup.Name);
        res.Description.ShouldBe(testGroup.Description);
        res.CreatedAt.ShouldBe(testGroup.CreatedAt);
    }

    [Fact, Priority(3)]
    public async Task Group_ShouldContainBookmarks()
    {

        const int groupId = 1492;

        using (var scope = App.Services.CreateScope())
        {
            var db = App.Services.GetRequiredService<AppDbContext>();
            db.BookmarkGroups.Add(
                new()
                {
                    Id = groupId,
                    Name = Guid.CreateVersion7().ToString(),
                    Description = null,
                    CreatedAt = DateTime.UtcNow,
                    OwnerId = App.UserBId!,
                });

            db.Bookmarks.Add(
                new()
                {
                    Id = 10000,
                    Name = Guid.CreateVersion7().ToString(),
                    Url = Fake.Internet.UrlWithPath(),
                    Description = null,
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedAt = DateTime.UtcNow,
                    IsFavourite = true,
                    OwnerId = App.UserBId!,
                    GroupId = groupId,
                });

            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }

        var (rsp, responseDto) = await App.UserBClient
                                          .GETAsync<GetBookmarkGroupByIdEndpoint, GetBookmarkGroupByIdRequest,
                                              GetBookmarkGroupByIdResponse>(
                                              new()
                                              {
                                                  Id = groupId,
                                              });

        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseDto.Id.ShouldBe(groupId);
        responseDto.Bookmarks.Count.ShouldBe(1);
    }

    [Fact, Priority(4)]
    public async Task AccessingGroup_FromOtherPeople_ShouldFail()
    {

        const int groupId = 5000;

        using (var scope = App.Services.CreateScope())
        {
            var db = App.Services.GetRequiredService<AppDbContext>();
            db.BookmarkGroups.Add(
                new()
                {
                    Id = groupId,
                    Name = Guid.CreateVersion7().ToString(),
                    Description = null,
                    CreatedAt = DateTime.UtcNow,
                    OwnerId = App.UserBId!,
                });
            await db.SaveChangesAsync(TestContext.Current.CancellationToken);
        }
        
        var (rsp, _) = await App.UserAClient.GETAsync<GetBookmarkGroupByIdEndpoint, GetBookmarkGroupByIdRequest,
                           GetBookmarkGroupByIdResponse>(
                           new()
                           {
                               Id = groupId,
                           });
            rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
