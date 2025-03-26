namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response returned after successfully creating a new cart.
/// </summary>
/// <remarks>
/// This response contains details of the newly created cart, including its unique identifier, associated user ID,
/// date of creation, and a list of products within the cart.
/// </remarks>
public class CreateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created cart.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created cart in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was created.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the list of products included in the cart.
    /// </summary>
    public List<CreateCartProductResult> Products { get; set; } = new List<CreateCartProductResult>();
}

/// <summary>
/// Represents a product within the cart.
/// </summary>
public class CreateCartProductResult
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