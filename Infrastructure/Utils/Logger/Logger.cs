using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.EndpointLog;
using Domain.Models.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Utils.Logger;

public class Logger : ILogger
{
    private const int ReadChunkBufferLength = 4096;
    private readonly bool _enabledLogging = Environment.GetEnvironmentVariable("EnabledLogging") == "true";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEndpointLogRepository _logRepository;

    public Logger(
        IEndpointLogRepository logRepository,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _logRepository = logRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    public async Task Log(string handlerName, object? request = null, object? response = null)
    {
        if (!_enabledLogging) return;
        object? stringResponse = response != null ? JsonConvert.SerializeObject(response) : null;
        var log = new EndpointLog(
            GetPath(),
            GetBody(),
            stringResponse ?? new { },
            200,
            GetAccessToken(),
            GetUserId(),
            GetElapsedTime(),
            GetMethod(),
            handlerName
        );
        await _logRepository.Save(log);
    }

    public async Task LogError(Exception exception, string handlerName)
    {
        if (!_enabledLogging) return;
        object response = exception.StackTrace != null ? new {exception.StackTrace} : new { };
        var log = new EndpointLog(
            GetPath(),
            GetBody(),
            response,
            200,
            GetAccessToken(),
            GetUserId(),
            GetElapsedTime(),
            GetMethod(),
            handlerName,
            exception.Message,
            exception.GetType().Name
        );
        await _logRepository.Save(log);
    }

    private string GetMethod()
    {
        return Context.Request.Method;
    }

    private TimeSpan GetElapsedTime()
    {
        var startTime = (DateTime) Context.Items["RequestStartTime"];
        return DateTime.Now - startTime;
    }

    private Guid? GetUserId()
    {
        var user = (User?) Context.Items["User"];
        return user?.Id;
    }

    private string GetPath()
    {
        return Context.Request.Path.Value.Split("/api/")[1];
    }

    private string? GetAccessToken()
    {
        Context.Request.Headers.TryGetValue("Authorization", out var header);
        var stringHeader = header.ToString();
        return stringHeader == string.Empty ? null : stringHeader.Split("Bearer ")[1];
    }

    private object GetBody()
    {
        if (Context.Request.Method == "GET") return new { };
        if (Context.Request.Path.Value.Contains("save-image"))
            return "Not available because body is binary form data image";
        var body = ReadStreamInChunks(Context.Request.Body);
        return body;
    }

    private string ReadStreamInChunks(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);
        var readChunk = new char[ReadChunkBufferLength];
        int readChunkLength;
        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, ReadChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);

        var result = textWriter.ToString();

        return result;
    }
}