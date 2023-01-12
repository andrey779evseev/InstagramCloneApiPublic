using FluentValidation;

namespace Application.Commands.Media.SaveImage;

public class SaveImageCommandValidator : AbstractValidator<SaveImageCommand>
{
    public SaveImageCommandValidator()
    {
        RuleFor(command => command.FileType).NotNull().IsInEnum();
    }
}