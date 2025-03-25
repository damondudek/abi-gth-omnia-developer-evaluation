namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// API response model for CreateProduct operation
/// </summary>
public class CreateProductResponse
{
    /// <summary>
    /// The id of the product.
    /// </summary>
    public string Id { get; set; }

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
