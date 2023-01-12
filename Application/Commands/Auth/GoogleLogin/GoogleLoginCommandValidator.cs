using FluentValidation;

namespace Application.Commands.Auth.GoogleLogin;

public class GoogleLoginCommandValidator : AbstractValidator<GoogleLoginCommand>
{
    public GoogleLoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.Name)
            .NotEmpty();
        RuleFor(command => command.Token)
            .NotEmpty();
    }
}