using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartDiscountTierOne : ICartBusinessRule
    {
        public decimal CalculateDiscount(CartProduct product, IDictionary<string, string> rules)
        {
            var minQuantityForDiscount = int.Parse(rules["MinQuantityForDiscount"], CultureInfo.InvariantCulture);
            var maxQuantityForTier1 = int.Parse(rules["MaxQuantityForDiscountTier1"], CultureInfo.InvariantCulture);
            var tier1Discount = decimal.Parse(rules["Tier1Discount"], CultureInfo.InvariantCulture);

            if (product.Quantity >= minQuantityForDiscount && product.Quantity <= maxQuantityForTier1)
                return tier1Discount;

            return 0;
        }
    }
}
