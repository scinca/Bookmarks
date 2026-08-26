using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.BookmarkGroups.Endpoints;

public class UpdateBookmarkGroupRequest
{
    [RouteParam]
    public int Id {get; init;}
    
    public string? Name {get; init;}
    public string? Description {get; init;}
    public bool? ChangedDescription {get; init;}
}


internal class UpdateBookmarkGroupRequestValidator : Validator<UpdateBookmarkGroupRequest>
{
    public UpdateBookmarkGroupRequestValidator()
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