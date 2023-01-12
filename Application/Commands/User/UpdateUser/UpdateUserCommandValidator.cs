using FluentValidation;

namespace Application.Commands.User.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.Nickname)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}