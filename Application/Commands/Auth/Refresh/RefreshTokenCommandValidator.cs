using FluentValidation;

namespace Application.Commands.Auth.Refresh;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .MaximumLength(500);
    }
}