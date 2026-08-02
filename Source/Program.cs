using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
   .AddFastEndpoints(DiscoveredTypes.All)
   .OpenApiDocument();



var app = builder.Build();

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints(
       c =>
       {
           c.Binding.ReflectionCache.AddFromBookmarks();
           c.Errors.UseProblemDetails();
       });
app.MapOpenApi();
app.MapScalarApiReference(o => o.AddDocuments("v1"));
app.Run();