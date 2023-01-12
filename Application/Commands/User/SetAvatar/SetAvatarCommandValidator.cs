using FluentValidation;

namespace Application.Commands.User.SetAvatar;

public class SetAvatarCommandValidator : AbstractValidator<SetAvatarCommand>
{
    public SetAvatarCommandValidator()
    {
        RuleFor(command => command.Url)
            .NotNull()
            .NotEmpty();
    }
}