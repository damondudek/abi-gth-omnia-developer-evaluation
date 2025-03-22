using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a category of products in the system.
/// Categories group products into logical classifications (e.g., Electronics, Clothing).
/// </summary>
public class ProductCategory : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// Must be unique and descriptive.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description for the category.
    /// Provides additional information about the category.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of products in this category.
    /// Indicates the products that belong to this category.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
