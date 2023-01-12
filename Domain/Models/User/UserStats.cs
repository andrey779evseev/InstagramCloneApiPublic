namespace Domain.Models.User;

public record UserStats(
    int FollowersCount,
    int FollowingCount,
    int PostsCount
);