using FluentValidation;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.GetById;

public class Request
{
    [RouteParam]
    public int Id {get; init;}
}


public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty() // Not empty also checks for the default int value 0.
            .WithMessage("Id must be provided and greater than 0");
        
    }
}