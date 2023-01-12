namespace Domain.Models.Like;

public record PostLikesInfo(
    string? FirstName,
    List<string> Avatars,
    int LikesCount,
    bool Liked
);