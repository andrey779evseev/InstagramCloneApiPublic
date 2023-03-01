using FluentValidation;

namespace Application.Validators;

public class UserPasswordValidator : AbstractValidator<string>
{
    public UserPasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lower");
    }
}