using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

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
            var maxQuantityLimit = _businessRulesRepository.GetValueAsInt("MaxQuantityLimit");
            var hasAnyProductOutOfRule = products.Any(product => product.Quantity > maxQuantityLimit);
            if (hasAnyProductOutOfRule)
                throw new InvalidOperationException($"Cannot sell more than {maxQuantityLimit} items of any product.");
        }

        public void ValidatePurchase(IEnumerable<CartProduct> products)
        {
            ValidateProducts(products);
            ValidateMaxPurchaseItems(products);

            var businessRules = _businessRulesRepository.GetAll();
            var cartBusinessRules = new List<ICartBusinessRule>()
            {
                new CartDiscountTierOne(),
                new CartDiscountTierTwo(),
            };

            foreach (var product in products)
            {
                var discount = cartBusinessRules.Select(rule => rule.CalculateDiscount(product, businessRules)).Max();
                product.Discount = discount;
            }
        }
    }

    public interface ICartRules
    {
        void ValidatePurchase(IEnumerable<CartProduct> products);
    }
}