using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Represents the response returned after successfully updating a cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the updated cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCartResult
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
    /// The date of the cart update
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The list of products in the cart
    /// </summary>
    public List<GetCartProductResult> Products { get; set; } = new List<GetCartProductResult>();
}