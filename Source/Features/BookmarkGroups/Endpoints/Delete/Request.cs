using FluentValidation;

namespace Bookmarks.Features.BookmarkGroups.Endpoints.Delete;

public class Request
{
    [RouteParam]
    public int GroupId { get; init; }
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .GreaterThan(0).WithMessage("GroupId must be provided and greater than 0");
    }
}