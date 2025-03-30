using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartCommand that defines validation rules for cart update operations.
/// </summary>
public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item><description>UserId: Must be valid and not null.</description></item>
    /// <item><description>Products: At least one product must exist in the cart.</description></item>
    /// <item><description>Each Product: Must include valid ProductId, Quantity, and optionally allow adjustments to Price.</description></item>
    /// </list>
    /// </remarks>
    public UpdateCartCommandValidator()
    {
        RuleFor(cart => cart.UserId).NotNull().WithMessage("UserId must be informed."); ;
        RuleFor(cart => cart.Products).NotEmpty().WithMessage("The cart must contain at least one product.");
        RuleForEach(cart => cart.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId).NotNull().WithMessage("ProductId must be informed.");
            product.RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });
    }
}