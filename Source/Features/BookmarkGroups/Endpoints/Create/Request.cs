using FluentValidation;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Create;

public class Request
{
    public string Name {get; init;}
    public string? Description {get; init;}
    
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("The name can not  exceed 100 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty. If you don't need a description use null")
            .MaximumLength(500).WithMessage("The description can not  exceed 500 characters")
            .When( x => x.Description is not null);
    }
}