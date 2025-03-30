using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartRules : ICartRules
    {
        private readonly IBusinessRuleRepository _businessRulesRepository;
        private readonly IProductRepository _productRepository;

        public CartRules(IBusinessRuleRepository businessRulesRepository, IProductRepository productRepository)
        {
            _businessRulesRepository = businessRulesRepository;
            _productRepository = productRepository;
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

        public async Task UpdateCartProductsInfo(IEnumerable<CartProduct> cartProducts, CancellationToken cancellationToken)
        {
            var productIds = cartProducts.Select(p => p.ProductId).Distinct().ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

            foreach (var cartProduct in cartProducts)
            {
                var product = products.FirstOrDefault(p => p.Id == cartProduct.ProductId) ?? throw new ValidationException($"Product {cartProduct.ProductId} not found");
                cartProduct.UpdateProductInfo(product);
            }
        }

        public async Task RemoveCartProducts(IEnumerable<CartProduct> cartProducts, CancellationToken cancellationToken)
        {
            var productIds = cartProducts.Select(p => p.ProductId).Distinct().ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

            foreach (var cartProduct in cartProducts)
            {
                var product = products.FirstOrDefault(p => p.Id == cartProduct.ProductId) ?? throw new ValidationException($"Product {cartProduct.ProductId} not found");
                cartProduct.UpdateProductInfo(product);
            }
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
    }

    public interface ICartRules
    {
        void ValidatePurchase(IEnumerable<CartProduct> products);
        Task UpdateCartProductsInfo(IEnumerable<CartProduct> cartProducts, CancellationToken cancellationToken);
    }
}