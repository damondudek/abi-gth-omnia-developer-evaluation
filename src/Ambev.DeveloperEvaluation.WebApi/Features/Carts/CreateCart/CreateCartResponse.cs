namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// API response model for CreateCart operation
/// </summary>
public class CreateCartResponse
{
    /// <summary>
    /// The unique identifier of the created cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user who owns the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The date when the cart was created
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The products included in the cart
    /// </summary>
    public List<CreateProductCartResponse> Products { get; set; } = new List<CreateProductCartResponse>();
}

/// <summary>
/// Represents a product in the cart
/// </summary>
public class CreateProductCartResponse
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product in the cart
    /// </summary>
    public int Quantity { get; set; }
}