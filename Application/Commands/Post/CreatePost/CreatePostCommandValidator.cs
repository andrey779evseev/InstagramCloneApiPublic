using FluentValidation;

namespace Application.Commands.Post.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(command => command.Description)
            .NotNull()
            .MaximumLength(2200);
        RuleFor(command => command.Photo)
            .NotNull()
            .NotEmpty();
    }
}