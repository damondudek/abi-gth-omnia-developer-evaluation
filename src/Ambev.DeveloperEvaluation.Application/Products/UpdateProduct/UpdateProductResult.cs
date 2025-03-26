namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a product.
/// </summary>
/// <remarks>
/// This response contains the details of the updated product,
/// including its identifier, title, price, description, category, image, and rating.
/// </remarks>
public class UpdateProductResult
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the product
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// A description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the product image
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The rating details of the product
    /// </summary>
    public ProductRating Rating { get; set; } = new ProductRating();
}

/// <summary>
/// Represents the rating details of a product
/// </summary>
public class ProductRating
{
    /// <summary>
    /// The average rating of the product
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// The total count of ratings for the product
    /// </summary>
    public int Count { get; set; }
}