namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public ApiResponseWithData()
    {
    }

    public ApiResponseWithData(T? data, string message) : base(message)
    {
        Data = data;
    }

    public T? Data { get; set; }
}
