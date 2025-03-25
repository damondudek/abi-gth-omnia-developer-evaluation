namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents the data required for a product request.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// The title of the product.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The category the product belongs to.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// The URL of the product image.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// The rating of the product.
    /// </summary>
    public CreateProductRatingRequest Rating { get; set; }
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
public class CreateProductRatingRequest
{
    /// <summary>
    /// The average rating score of the product.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// The total count of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
