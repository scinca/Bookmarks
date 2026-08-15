using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Hosting;

namespace Tests;

public class App : AppFixture<Program>
{
    public HttpClient UserAClient { get; private set; } = null!;
    public HttpClient UserBClient { get; private set; } = null!;
    
    protected override async ValueTask SetupAsync()
    {
        // place one-time setup for the fixture here
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
        // do cleanups here
        return ValueTask.CompletedTask;
    }
    
    public async Task<HttpClient> CreateAuthedClientAsync(string email, string password = "A#1!StrongPassword")
    {
        var anonymous = CreateClient();
        await anonymous.PostAsJsonAsync("/register", new { email, password });
        
        var loginResponse = await anonymous.PostAsJsonAsync("/login", new {email, password});
        var token = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
        
        return CreateClient(c => c.DefaultRequestHeaders.Authorization= new("Bearer", token!.AccessToken));
    }
}