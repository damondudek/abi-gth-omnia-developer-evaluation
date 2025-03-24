namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiErrorResponse<T>
{
    public ApiErrorResponse()
    {
    }

    public ApiErrorResponse(string type, string error, T details)
    {
        Type = type;
        Error = error;
        Details = details;
    }

    public string Type { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public T Details { get; set; }
}
