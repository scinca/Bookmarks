using System.Net.Http.Json;
using Bookmarks;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Hosting;

namespace Tests;

public class App : AppFixture<Program>
{
    public HttpClient UserAClient { get; private set; } = null!;
    public HttpClient UserBClient { get; private set; } = null!;
    
    protected override async ValueTask SetupAsync()
    {
        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();
        }
        
        UserAClient = await CreateAuthedClientAsync("user-a@test.com");
        UserBClient = await CreateAuthedClientAsync("user-b@test.com");
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        // do host builder configuration here
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        // do test service registration here
    }

    protected override ValueTask TearDownAsync()
    {
        UserAClient.Dispose();
        UserBClient.Dispose();
        // do cleanups here
        return ValueTask.CompletedTask;
    }
    
    public async Task<HttpClient> CreateAuthedClientAsync(string email, string password = "A#1!StrongPassword")
    {
        var anonymous = CreateClient();
        await anonymous.PostAsJsonAsync("api/auth/register", new { email, password });
        
        var loginResponse = await anonymous.PostAsJsonAsync("api/auth/login", new {email, password});
        var token = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
        
        return CreateClient(c => c.DefaultRequestHeaders.Authorization= new("Bearer", token!.AccessToken));
    }
}