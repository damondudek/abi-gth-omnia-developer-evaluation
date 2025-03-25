namespace Ambev.DeveloperEvaluation.Domain.Models;

public class ApiPaginatedResponse<T> : ApiDataResponse<IEnumerable<T>>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}