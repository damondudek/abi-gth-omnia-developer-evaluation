using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the system with details such as title, price, description, and category.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
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
    /// Gets or sets the category the product belongs to.
    /// This represents a many-to-one relationship between products and categories.
    /// </summary>
    public Guid ProductCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the denormalized category name for quick access.
    /// The name should provide a snapshot of the category description from another domain.
    /// </summary>
    public string ProductCategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the product image.
    /// Must be a valid URL.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating details of the product.
    /// Includes both the average rating and the number of reviews.
    /// </summary>
    //public Rating Rating { get; set; } = new Rating();

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

/// <summary>
/// Represents the rating information for a product, including average rate and review count.
/// </summary>
public class Rating
{
    /// <summary>
    /// Gets or sets the average rating for the product.
    /// Must be a number between 0 and 5.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// Gets or sets the number of reviews for the product.
    /// Must be a non-negative integer.
    /// </summary>
    public int Count { get; set; }
}
