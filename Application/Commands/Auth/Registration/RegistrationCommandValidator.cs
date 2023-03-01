using Application.Validators;
using FluentValidation;

namespace Application.Commands.Auth.Registration;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(command => command.Password)
            .SetValidator(new UserPasswordValidator());
        RuleFor(command => command.Nickname)
            .NotEmpty()
            .MaximumLength(30);
    }
}