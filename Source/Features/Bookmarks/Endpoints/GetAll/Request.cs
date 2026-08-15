using Bookmarks.Common.PagedResponse;
using FluentValidation;

namespace Bookmarks.Features.Bookmarks.Endpoints.GetAll;

public class Request
{
    [QueryParam]
    public bool? IsArchived {get; init;}
    [QueryParam]
    public bool? IsFavourite {get; init;}
    [QueryParam]
    public int PageNumber { get; init; }
    [QueryParam]
    public PageSize PageSize {get; init;}
}

public class GetAllValidator : Validator<Request>
{
    public GetAllValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than zero");
        
        RuleFor(x => x.PageSize)
            .IsInEnum()
            .WithMessage($"PageSize must be in Enum. Possible values are: {PageSize.Small}, {PageSize.Normal} and {PageSize.Large}");
    }
}