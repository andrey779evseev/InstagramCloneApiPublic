using FluentValidation;

namespace Application.Commands.Friendships.SearchUsers;

public class SearchUsersCommandValidator : AbstractValidator<SearchUsersCommand>
{
    public SearchUsersCommandValidator()
    {
        RuleFor(command => command.Search)
            .MaximumLength(50);
        RuleFor(command => command.Take)
            .GreaterThan(0)
            .LessThanOrEqualTo(50);
    }
}