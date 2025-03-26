namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents a request to create a new cart in the system.
/// </summary>
public class CreateCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the products in the cart.
    /// </summary>
    public List<CreateCartProductRequest> Products { get; set; } = new List<CreateCartProductRequest>();
}

/// <summary>
/// Represents a product in the cart.
/// </summary>
public class CreateCartProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }
}