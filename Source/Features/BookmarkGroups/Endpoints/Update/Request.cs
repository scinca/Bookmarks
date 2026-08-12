using FluentValidation;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Update;

public class Request
{
    [RouteParam]
    public int GroupId {get; init;}
    
    public string? Name {get; init;}
    public string? Description {get; init;}
    public bool? ChangedDescription {get; init;}
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("GroupId should be greater than 0");
        
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