using Domain.Models.User;

namespace Domain.Models.Comment;

public record CommentDetail(
    Guid CommentId,
    Guid PostId,
    UserMiniature Author,
    string Text,
    DateTime CommentedAt
);