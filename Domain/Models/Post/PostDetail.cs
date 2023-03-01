using Domain.Models.Like;
using Domain.Models.User;

namespace Domain.Models.Post;

public record PostDetail(
    Guid Id,
    UserMiniature Author,
    string Description,
    string Photo,
    DateTime PostedAt,
    PostLikesInfo LikesInfo
);