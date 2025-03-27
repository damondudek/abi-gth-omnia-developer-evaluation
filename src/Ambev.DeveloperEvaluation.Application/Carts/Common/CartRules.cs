using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartRules : ICartRules
    {
        private readonly IBusinessRuleRepository _businessRulesRepository;

        public CartRules(IBusinessRuleRepository businessRulesRepository)
        {
            _businessRulesRepository = businessRulesRepository;
        }

        private void ValidateProducts(IEnumerable<CartProduct> products)
        {
            if (products == null || !products.Any())
                throw new ArgumentException("The product list cannot be null or empty.", nameof(products));
        }

        private void ValidateMaxPurchaseItems(IEnumerable<CartProduct> products)
        {
            var maxQuantityLimit = _businessRulesRepository.GetConfigValueAsIntegerByConfigKey("MaxQuantityLimit");
            var hasAnyProductOutOfRule = products.Any(product => product.Quantity > maxQuantityLimit);
            if (hasAnyProductOutOfRule)
                throw new InvalidOperationException($"Cannot sell more than {maxQuantityLimit} items of any product.");
        }

        public void ValidatePurchase(IEnumerable<CartProduct> products)
        {
            ValidateProducts(products);
            ValidateMaxPurchaseItems(products);

            var businessRules = _businessRulesRepository.GetAll();
            var minQuantityForDiscount = int.Parse(businessRules["MinQuantityForDiscount"], CultureInfo.InvariantCulture);
            var maxQuantityForTier1 = int.Parse(businessRules["MaxQuantityForDiscountTier1"], CultureInfo.InvariantCulture);
            var minQuantityForTier2 = int.Parse(businessRules["MinQuantityForDiscountTier2"], CultureInfo.InvariantCulture);
            var maxQuantityLimit = int.Parse(businessRules["MaxQuantityLimit"], CultureInfo.InvariantCulture);
            var tier1Discount = decimal.Parse(businessRules["Tier1Discount"], CultureInfo.InvariantCulture);
            var tier2Discount = decimal.Parse(businessRules["Tier2Discount"], CultureInfo.InvariantCulture);

            foreach (var product in products)
            {
                if (product.Quantity < minQuantityForDiscount)
                {
                    product.Discount = 0m;
                    continue;
                }

                if (product.Quantity >= minQuantityForDiscount && product.Quantity <= maxQuantityForTier1)
                    product.Discount = tier1Discount;

                if (product.Quantity >= minQuantityForTier2 && product.Quantity <= maxQuantityLimit)
                    product.Discount = tier2Discount;
            }
        }
    }

    public interface ICartRules
    {
        void ValidatePurchase(IEnumerable<CartProduct> products);
    }
}