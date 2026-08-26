using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Features.Bookmarks.Endpoints;

public class UpdateBookmarkRequest
{
    [RouteParam]
    public int Id {get; init;}
    
    public string? Name {get; init;}
    
    public string? Url {get; init;}
    
    public string? Description {get; init;}
    
    public bool? DescriptionChanged {get; init;}
    
    public bool? IsArchived {get; set;}
    public bool? IsFavourite {get; set;}
    
    public int? GroupId {get; init;}
    public bool? GroupChanged { get; init; }
}

internal class UpdateBookmarkRequestValidator : Validator<UpdateBookmarkRequest>
{
    public UpdateBookmarkRequestValidator()
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
                Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Url must be a valid absolute URL")
            .When(request => request.Url is not null);
        
        RuleFor(request => request.Description)
            .NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
            .When(request => request.Description is not null && request.GroupChanged is true);
        
        RuleFor(request => request.GroupId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(
                async (GroupId, ct) 
                    =>
                {
                    var context = Resolve<AppDbContext>();
                    return await context.BookmarkGroups.AnyAsync(
                               group => group.Id == GroupId,
                               cancellationToken: ct);
                })
            .WithMessage("Group not found")
            .When(request => request.GroupId is not null);
        
        
    }
}