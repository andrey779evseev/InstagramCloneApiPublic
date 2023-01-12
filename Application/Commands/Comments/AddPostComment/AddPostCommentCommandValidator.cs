using FluentValidation;

namespace Application.Commands.Comments.AddPostComment;

public class AddPostCommentCommandValidator : AbstractValidator<AddPostCommentCommand>
{
    public AddPostCommentCommandValidator()
    {
        RuleFor(command => command.Text)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1000);
    }
}