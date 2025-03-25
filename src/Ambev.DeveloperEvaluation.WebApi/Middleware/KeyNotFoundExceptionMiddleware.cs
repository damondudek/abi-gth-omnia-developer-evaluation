using Ambev.DeveloperEvaluation.Domain.Consts.Errors;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class KeyNotFoundExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public KeyNotFoundExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleKeyNotFoundExceptionAsync(context, ex);
        }
    }

    private static Task HandleKeyNotFoundExceptionAsync(HttpContext context, KeyNotFoundException exception)
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