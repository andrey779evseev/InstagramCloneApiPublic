namespace Domain.Interfaces.Utils.Logger;

public interface ILogger
{
    public Task Log(string handlerName, object? request = null, object? response = null);
    public Task LogError(Exception exception, string handlerName);
}