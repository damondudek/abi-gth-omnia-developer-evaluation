namespace Ambev.DeveloperEvaluation.Domain.Models;

public class ApiDataResponse<T> : ApiResponse
{
    public ApiDataResponse()
    {
    }

    public ApiDataResponse(T? data, string message) : base(message)
    {
        Data = data;
    }

    public T? Data { get; set; }
}
