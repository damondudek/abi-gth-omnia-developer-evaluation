using Ambev.DeveloperEvaluation.Domain.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public class PaginatedCommand<T> : IRequest<PaginatedResponse<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public Dictionary<string, string> Filters { get; set; } = [];
    }
}
