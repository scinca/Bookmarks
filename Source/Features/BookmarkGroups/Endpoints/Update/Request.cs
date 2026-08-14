using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Update;

public class Request
{
    [RouteParam]
    public int Id {get; init;}
    
    public string? Name {get; init;}
    public string? Description {get; init;}
    public bool? ChangedDescription {get; init;}
}


public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (Id, ct) 
                =>
            {
                var context = Resolve<AppDbContext>();
                return await context.BookmarkGroups
                                    .AnyAsync(group => group.Id == Id, cancellationToken: ct);
            })
            .WithMessage("Group not found");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name should be maximum 100 characters or less")
            .When(x => !string.IsNullOrEmpty(x.Name));
        
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => x.ChangedDescription is true);
    }
}