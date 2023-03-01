namespace Domain.Models.Post;

public record PostMiniature(
    Guid Id,
    string Photo,
    DateTime PostedAt,
    int LikesCount,
    int CommentsCount
);