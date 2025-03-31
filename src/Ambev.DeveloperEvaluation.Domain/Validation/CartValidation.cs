using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for Cart that defines validation rules for cart entity.
    /// </summary>
    public class CartValidator : AbstractValidator<Cart>
    {
        /// <summary>
        /// Initializes a new instance of the CartValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - UserId: Must not be empty or null
        /// - Products: Must contain at least one product
        /// - ProductId: Must not be empty or null
        /// - Quantity: Must be greater than zero
        /// </remarks>
        public CartValidator()
        {
            RuleFor(cart => cart.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("User ID is required");

            RuleFor(cart => cart.Products)
                .NotEmpty()
                .WithMessage("At least one product is required");

            RuleForEach(cart => cart.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Product ID is required");

                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero");
            });
        }
    }
}
