namespace Domain.Settings.Utils.Tokens;

public class JwtSettings
{
    public string AccessTokenSecret { get; set; } = null!;
    public string RefreshTokenSecret { get; set; } = null!;
    public double AccessTokenExpirationMinutes { get; set; } = default;
    public double RefreshTokenExpirationMinutes { get; set; } = default;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}