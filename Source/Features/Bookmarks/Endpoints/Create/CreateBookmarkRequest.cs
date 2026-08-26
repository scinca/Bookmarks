using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints;

public class CreateBookmarkRequest
{
    public string? Name {get; set;}
    public string Url {get; set;}
    public string? Description {get; set;}
    
    public int? GroupId {get; set;}
    
    [FromClaim(ClaimTypes.NameIdentifier)] 
    public string UserId {get; set;}
}


internal class CreateBookmarkRequestValidator : Validator<CreateBookmarkRequest>
{
    public CreateBookmarkRequestValidator()
    {
        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(2048).WithMessage("Name cannot exceed 2048 characters")
            .When(request => request.Name is not null);

        RuleFor(request => request.Url)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Url cannot be empty")
            .Must(url =>
                Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Url must be a valid absolute URL");
        
        RuleFor(request => request.Description)
            .NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
            .When(request => request.Description is not null);
        
        RuleFor(request => request.GroupId)
            .MustAsync(
                async (GroupId, ct) 
                =>
                {
                    var context =  Resolve<AppDbContext>();
                    return await context.BookmarkGroups.AnyAsync(
                               group => group.Id == GroupId,
                               cancellationToken: ct);
                }).WithMessage("Group not found")
            .When(request => request.GroupId is not null);
    }
}