using FluentValidation;

namespace Application.Queries.User.CheckNickname;

public class CheckNicknameQueryValidator : AbstractValidator<CheckNicknameQuery>
{
    public CheckNicknameQueryValidator()
    {
        RuleFor(query => query.Nickname)
            .NotEmpty()
            .MaximumLength(30);
    }
}