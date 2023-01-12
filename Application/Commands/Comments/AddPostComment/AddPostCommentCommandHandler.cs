using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Models.Comment;
using MediatR;

namespace Application.Commands.Comments.AddPostComment;

public class AddPostCommentCommandHandler : IRequestHandler<AddPostCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;

    public AddPostCommentCommandHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository,
        ICommentRepository commentRepository
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Unit> Handle(AddPostCommentCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var post = await _postRepository.OneById((Guid) command.PostId!, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "id");

        var comment = new Comment(command.Text, user.Id, post.Id);

        await _commentRepository.Save(comment, cancellationToken);

        return Unit.Value;
    }
}