using FluentValidation;

namespace Application.Queries.Posts.GetMiniatures;

public class GetMiniaturesQueryValidator : AbstractValidator<GetMiniaturesQuery>
{
    public GetMiniaturesQueryValidator()
    {
        RuleFor(query => query.Take)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}