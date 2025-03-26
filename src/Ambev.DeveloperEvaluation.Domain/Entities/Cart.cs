using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart in the system, containing products added by a user.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of products in the cart.
    /// </summary>
    public ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();

    /// <summary>
    /// Validates the cart entity using business rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed.
    /// - Errors: Collection of validation errors if any rules failed.
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">UserId validity</list>
    /// <list type="bullet">Product list consistency</list>
    /// <list type="bullet">Cart creation/modification date validity</list>
    /// </remarks>
    // public ValidationResultDetail Validate()
    // {
    //     var validator = new CartValidator();
    //     var result = validator.Validate(this);
    //     return new ValidationResultDetail
    //     {
    //         IsValid = result.IsValid,
    //         Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
    //     };
    // }
}