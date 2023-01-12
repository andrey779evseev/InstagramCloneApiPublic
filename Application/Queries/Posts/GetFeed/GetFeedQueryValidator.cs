using FluentValidation;

namespace Application.Queries.Posts.GetFeed;

public class GetFeedQueryValidator : AbstractValidator<GetFeedQuery>
{
    public GetFeedQueryValidator()
    {
        RuleFor(query => query.Take)
            .GreaterThan(0)
            .LessThanOrEqualTo(50);
    }
}