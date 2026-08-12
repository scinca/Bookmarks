namespace Bookmarks.Features.Bookmarks.Endpoints.GetById;

public class Endpoint(AppDbContext context) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bookmark");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await Send.NoContentAsync(cancellation: ct);
    }
}