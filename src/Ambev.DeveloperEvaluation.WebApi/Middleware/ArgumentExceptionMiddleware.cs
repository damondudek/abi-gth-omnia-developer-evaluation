using Ambev.DeveloperEvaluation.Domain.Const;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ArgumentExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ArgumentExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await HandleArgumentExceptionAsync(context, ex);
        }
    }

    private static Task HandleArgumentExceptionAsync(HttpContext context, ArgumentException exception)
    {
        var response = new ApiErrorResponse<string>
        {
            Type = ErrorType.ValidationError,
            Error = "Invalid argument",
            Details = exception.Message
        };

        return JsonCamelCaseResponse.HandleResponseAsync(context, StatusCodes.Status400BadRequest, response);
    }
}