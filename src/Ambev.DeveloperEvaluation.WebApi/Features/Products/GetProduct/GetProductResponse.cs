namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse
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
    public ProductRatingResponse Rating { get; set; } = new ProductRatingResponse();
}

/// <summary>
/// Represents the rating details of a product
/// </summary>
public class ProductRatingResponse
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