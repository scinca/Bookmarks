using Bookmarks.Features.User;
using Bookmarks.Features.User.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthorization();
builder.Services
       .AddIdentityApiEndpoints<ApplicationUser>()
       .AddEntityFrameworkStores<AppDbContext>();
    

builder.Services
   .AddFastEndpoints(DiscoveredTypes.All)
   .OpenApiDocument(options =>
   {
       options.ExcludeNonFastEndpoints = true;
   });


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

app.MapGroup("api/auth").MapIdentityApi<ApplicationUser>().WithTags("ignore", "auth");

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints(
       c =>
       {
           c.Binding.ReflectionCache.AddFromBookmarks();
           c.Errors.UseProblemDetails();
       });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o => o.AddDocuments("v1"));
}

app.Run();