namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user associated with the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user`s full name associated with the cart
    /// </summary>
    public string UserFullName { get; set; } = string.Empty;

    /// <summary>
    /// The date of the cart
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The list of products in the cart
    /// </summary>
    public List<GetCartProductResult> Products { get; set; } = new List<GetCartProductResult>();
}

/// <summary>
/// Represents a product in the cart
/// </summary>
public class GetCartProductResult
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
    /// The quantity of the product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The discount of the product
    /// </summary>
    public int Discount { get; set; }
}