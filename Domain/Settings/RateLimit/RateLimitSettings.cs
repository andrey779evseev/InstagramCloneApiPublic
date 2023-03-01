// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Domain.Settings.RateLimit;

public sealed class RateLimitSettings
{
    public int Window { get; set; }
    public int QueueLimit { get; set; }
    public int PermitLimit { get; set; }
    public bool AutoReplenishment { get; set; }
}