using FluentValidation;

namespace Application.Commands.Auth.Revoke;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken).NotEmpty();
    }
}