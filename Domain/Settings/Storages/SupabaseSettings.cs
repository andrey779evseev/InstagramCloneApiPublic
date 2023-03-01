namespace Domain.Settings.Storages;

public sealed class SupabaseSettings
{
    public string Url { get; set; } = null!;
    public string AnonKey { get; set; } = null!;
}