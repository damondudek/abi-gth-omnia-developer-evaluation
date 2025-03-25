namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created user.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created user in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the product.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// A brief description of the product.
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
    /// The rating details of the product.
    /// </summary>
    public CreateProductRatingCommand Rating { get; set; } = new CreateProductRatingCommand();
}
