using Ambev.DeveloperEvaluation.Domain.Const;
using Ambev.DeveloperEvaluation.Domain.Models;
using FluentValidation;
using System.Collections;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        var response = new ApiErrorResponse<IEnumerable>
        {
            Type = ErrorType.ValidationError,
            Error = "Invalid input data",
            Details = exception.Errors
                .Select(error => error.ErrorMessage)
        };

        return JsonCamelCaseResponse.HandleResponseAsync(context, StatusCodes.Status400BadRequest, response);
    }
}
