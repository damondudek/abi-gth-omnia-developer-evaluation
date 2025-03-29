using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartDiscountTierTwo : ICartBusinessRule
    {
        public decimal CalculateDiscount(CartProduct product, IDictionary<string, string> rules)
        {
            var minQuantityForTier2 = int.Parse(rules["MinQuantityForDiscountTier2"], CultureInfo.InvariantCulture);
            var maxQuantityLimit = int.Parse(rules["MaxQuantityLimit"], CultureInfo.InvariantCulture);
            var tier2Discount = decimal.Parse(rules["Tier2Discount"], CultureInfo.InvariantCulture);

            if (product.Quantity >= minQuantityForTier2 && product.Quantity <= maxQuantityLimit)
                return tier2Discount;

            return 0;
        }
    }
}
