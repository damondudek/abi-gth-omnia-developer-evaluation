using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartCommand that defines validation rules for cart creation command.
/// </summary>
public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: Must be not null
    /// - Products: At least one product must be present
    /// - Each Product: Must include valid ProductId and Quantity
    /// </remarks>
    public CreateCartCommandValidator()
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