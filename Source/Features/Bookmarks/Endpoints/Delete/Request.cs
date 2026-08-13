using FluentValidation;

namespace Bookmarks.Features.Bookmarks.Endpoints.Delete;

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
            .GreaterThan(0).WithMessage("Id must be greater than 0");
        
    }
}