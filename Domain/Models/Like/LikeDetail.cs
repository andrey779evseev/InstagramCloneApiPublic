using Domain.Models.User;

namespace Domain.Models.Like;

public record LikeDetail(
    Guid Id,
    Guid PostId,
    ExtendedUserMiniature User
);