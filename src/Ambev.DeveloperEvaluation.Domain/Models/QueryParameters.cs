using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Domain.Models
{
    public class QueryParameters
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        [FromQuery(Name = "_page")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "_size")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        [FromQuery(Name = "_order")]
        public string OrderBy { get; set; } = string.Empty;
    }

}
