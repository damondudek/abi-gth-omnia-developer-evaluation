namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// API response model for GetCart operation
/// </summary>
public class GetCartResponse
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user who owns the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user`s full name associated with the cart
    /// </summary>
    public string UserFullName { get; set; } = string.Empty;

    /// <summary>
    /// The date the cart was created or updated
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// A list of products in the cart
    /// </summary>
    public List<GetCartProductResponse> Products { get; set; } = new List<GetCartProductResponse>();
}

/// <summary>
/// Represents a product in the cart
/// </summary>
public class GetCartProductResponse
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The identifier of the product`s title
    /// </summary>
    public string ProductTitle { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the product`s price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of the product in the cart
    /// </summary>
    public int Quantity { get; set; }
}