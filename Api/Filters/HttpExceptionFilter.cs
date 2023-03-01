using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Domain.Interfaces.Utils.Logger.ILogger;

namespace Api.Filters;

public class HttpExceptionFilter : IAsyncActionFilter
{
    private readonly ILogger _logger;

    public HttpExceptionFilter(
        ILogger logger
    )
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();
        if (executedContext.Exception == null) return;
        executedContext.Result = executedContext.Exception switch
        {
            NotFoundException ex => new NotFoundObjectResult(ex.Message),
            EntityExistsException ex1 => new ConflictObjectResult(ex1.Message),
            ValidationRequestException ex2 => new UnprocessableEntityObjectResult(ex2.Message),
            InvalidRefreshTokenException ex3 => new UnauthorizedObjectResult(ex3.Message),
            UserDoesNotExistException ex4 => new UnauthorizedObjectResult(ex4.Message),
            TokenAlreadyRevokedException ex5 => new OkObjectResult(ex5.Message),
            _ => new BadRequestObjectResult(executedContext.Exception?.Message)
        };
        executedContext.ExceptionHandled = true;
        var fullName = executedContext
            .Exception
            ?.TargetSite
            ?.ReflectedType
            ?.DeclaringType
            ?.FullName
            ?.Split(".")
            .LastOrDefault();
        var handlerName = fullName == null ? "" : fullName.Replace("Handler", "");
        await _logger.LogError(executedContext.Exception!, handlerName);
    }
}