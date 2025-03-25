namespace Ambev.DeveloperEvaluation.Domain.Models
{
    public class PaginatedResponse<T>
    {
        public IList<T> Data { get; set; } = [];
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
