using Bookmarks;

namespace Tests.BookmarkGroups.CreateBookmarkGroupTests;

public class DeleteBookmarkGroupsTests(App App) : TestBase<App>
{
    protected override ValueTask SetupAsync()
    {
        var dummyData = []
        
        
        using var scope = App.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        
    }
}