using Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ValidationBehavior(
        IHttpContextAccessor contextAccessor
    )
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator =
            (IValidator<TRequest>?) _contextAccessor.HttpContext!.RequestServices.GetService(
                typeof(IValidator<TRequest>));
        if (validator != null)
        {
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
                throw new ValidationRequestException(result.Errors.Select(err => err.ErrorMessage));
        }

        var response = await next();
        return response;
    }
}