using FluentValidation;

namespace Application.Queries.Friendships.GetSuggestions;

public class GetSuggestionsQueryValidator : AbstractValidator<GetSuggestionsQuery>
{
    public GetSuggestionsQueryValidator()
    {
        RuleFor(query => query.Take)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}