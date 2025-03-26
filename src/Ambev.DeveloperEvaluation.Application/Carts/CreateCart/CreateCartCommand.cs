using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a cart, 
/// including user ID, date, and product details. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateCartResult"/>.
///
/// The data provided in this command is validated using the 
/// <see cref="CreateCartCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateCartCommand : IRequest<CreateCartResult>
{
    /// <summary>
    /// The unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The product details included in the cart.
    /// </summary>
    public List<CreateCartProductCommand> Products { get; set; } = new List<CreateCartProductCommand>();

    /// <summary>
    /// Validates the command object using the <see cref="CreateCartCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> object containing validation results.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the product details within a cart.
/// </summary>
public class CreateCartProductCommand
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }
}