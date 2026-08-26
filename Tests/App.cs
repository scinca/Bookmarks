using System.Net.Http.Json;
using Bogus;
using Bookmarks;
using Bookmarks.Features.BookmarkGroups;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tests;

public class App : AppFixture<Program>
{
    public HttpClient UserAClient { get; private set; } = null!;
    public string? UserAId { get; private set; }
    public HttpClient UserBClient { get; private set; } = null!;
    public string? UserBId { get; private set; }
    
    protected override async ValueTask SetupAsync()
    {
        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();
        }
        
        (UserAClient, UserAId) = await CreateAuthenticatedClientAsync("user-a@test.com");
        (UserBClient, UserBId) = await CreateAuthenticatedClientAsync("user-b@test.com");
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        // do host builder configuration here
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        s.RemoveAll<AppDbContext>();
        var dbName = $"test_{Guid.NewGuid()}.db";
        s.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dbName};");
        });
    }

    protected override async ValueTask TearDownAsync()
    {
        UserAClient.Dispose();
        UserBClient.Dispose();

        using var scope = Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureDeletedAsync();
    }
    
    public async Task<(HttpClient Client,string? UserId)> CreateAuthenticatedClientAsync(string email, string password = "A#1!StrongPassword")
    {
        using var anonymous = CreateClient();
        await anonymous.PostAsJsonAsync("api/auth/register", new { email, password });
        
        var loginResponse = await anonymous.PostAsJsonAsync("api/auth/login", new {email, password});
        var token = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
        
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userId = db.Users.Where(u => u.Email == email).Select(u => u.Id).FirstOrDefault();
        
        return (CreateClient(c => c.DefaultRequestHeaders.Authorization= new("Bearer", token!.AccessToken)), userId);
    }

    public static Faker<BookmarkGroup> BookmarkGroupFaker(string?[] ownerIds)
        => new Faker<BookmarkGroup>()
           .RuleFor(bg => bg.Name, f => f.Lorem.Word() + f.Random.String2(20))
           .RuleFor(bg => bg.Description, f => f.Random.Bool() ? f.Lorem.Sentence() : null)
           .RuleFor(bg => bg.CreatedAt, f => f.Date.Recent(10))
           .RuleFor(bg => bg.OwnerId, f => f.PickRandom(ownerIds));

}