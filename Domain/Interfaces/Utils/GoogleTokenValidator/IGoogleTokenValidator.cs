namespace Domain.Interfaces.Utils.GoogleTokenValidator;

public interface IGoogleTokenValidator
{
    public Task<string?> GetId(string token);
}