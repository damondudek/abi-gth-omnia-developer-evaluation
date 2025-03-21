using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponse
{
    public ApiResponse()
    {
    }

    public ApiResponse(string message)
    {
        Message = message;
    }

    public ApiResponse(string message, bool success)
    {
        Message = message;
        Success = success;
    }

    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
