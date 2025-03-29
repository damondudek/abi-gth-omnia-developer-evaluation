using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public interface ICartBusinessRule
    {
        decimal CalculateDiscount(CartProduct product, IDictionary<string, string> rules);
    }
}
