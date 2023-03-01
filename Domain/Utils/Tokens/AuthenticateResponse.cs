namespace Domain.Utils.Tokens;

public record AuthenticateResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);