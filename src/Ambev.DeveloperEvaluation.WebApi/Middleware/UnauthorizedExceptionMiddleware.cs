using Ambev.DeveloperEvaluation.Domain.Consts.Errors;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class UnauthorizedExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            await HandleUnauthorizedExceptionAsync(context);
    }

    private static Task HandleUnauthorizedExceptionAsync(HttpContext context)
    {
        var response = new ApiErrorResponse<string>
        {
            Type = ErrorType.AuthenticationError,
            Error = "Invalid authentication token",
            Details = "The provided authentication token has expired or is invalid"
        };

        return JsonCamelCaseResponse.HandleResponseAsync(context, StatusCodes.Status401Unauthorized, response);
    }
}
