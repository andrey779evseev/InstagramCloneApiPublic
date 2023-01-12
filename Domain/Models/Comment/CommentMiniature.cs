namespace Domain.Models.Comment;

public record CommentMiniature(
    Guid Id,
    Guid PostId,
    string AuthorName,
    string Text
);