using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the system with details such as title, price, description, and category.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the list of products in the cart.
    /// </summary>
    public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    /// <summary>
    /// Gets or sets the title of the product.
    /// Must not be null or empty.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// Must be a positive number.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// Provides additional details about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the denormalized category name for quick access.
    /// The name should provide a snapshot of the category description from another domain.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the product image.
    /// Must be a valid URL.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the average rating for the product.
    /// Must be a number between 0 and 5.
    /// </summary>
    public double AverageRating { get; set; }

   /// <summary>
    /// Gets or sets the number of reviews for the product.
    /// Must be a non-negative integer.
    /// </summary>
    public double RatingCount { get; set; }

    /// <summary>
    /// Validates the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed.
    /// - Errors: Collection of validation errors if any rules failed.
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title format and length</list>
    /// <list type="bullet">Price validity</list>
    /// <list type="bullet">Description length</list>
    /// <list type="bullet">Category validity</list>
    /// <list type="bullet">Image URL validity</list>
    /// </remarks>
    //public ValidationResultDetail Validate()
    //{
    //    var validator = new ProductValidator();
    //    var result = validator.Validate(this);
    //    return new ValidationResultDetail
    //    {
    //        IsValid = result.IsValid,
    //        Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
    //    };
    //}
}
