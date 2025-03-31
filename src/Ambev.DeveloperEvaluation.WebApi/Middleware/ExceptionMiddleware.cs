using Ambev.DeveloperEvaluation.Domain.Const;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ApiErrorResponse<string>
        {
            Type = ErrorType.InternalServerError,
            Error = "Internal server error",
            Details = exception.Message
        };

        return JsonCamelCaseResponse.HandleResponseAsync(context, StatusCodes.Status500InternalServerError, response);
    }
}