using Application.Validators;
using FluentValidation;

namespace Application.Commands.Auth.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(command => command.OldPassword)
            .SetValidator(new UserPasswordValidator());
        RuleFor(command => command.NewPassword)
            .SetValidator(new UserPasswordValidator())
            .NotEqual(command => command.OldPassword);
    }
}