using Newtonsoft.Json;

namespace Domain.Models.EndpointLog;

public class EndpointLog
{
    public EndpointLog(
        string path,
        object request,
        object response,
        int statusCode,
        string? accessToken,
        Guid? userId,
        TimeSpan elapsedTime,
        string method, string handlerName, string? errorDescription = null,
        string? exceptionName = null
    )
    {
        Id = Guid.NewGuid();
        Path = path;
        Request = request;
        Response = response;
        RespondedAt = DateTime.Now;
        StatusCode = statusCode;
        AccessToken = accessToken;
        UserId = userId;
        ElapsedTime = elapsedTime;
        Method = method;
        HandlerName = handlerName;
        ErrorDescription = errorDescription;
        ExceptionName = exceptionName;
    }

    public Guid Id { get; }
    public string? AccessToken { get; }
    public Guid? UserId { get; }
    public string Path { get; }
    public object Request { get; }
    public object Response { get; }
    public DateTime RespondedAt { get; }
    public int StatusCode { get; }
    public TimeSpan ElapsedTime { get; }
    public string Method { get; }
    public string HandlerName { get; }
    public string? ErrorDescription { get; }
    public string? ExceptionName { get; }
    public bool IsError => StatusCode >= 300;


    //relationships
    [JsonIgnore] public virtual User.User User { get; } = null!;
}