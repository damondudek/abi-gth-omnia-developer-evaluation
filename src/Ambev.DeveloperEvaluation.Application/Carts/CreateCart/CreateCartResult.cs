using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response returned after successfully creating a new cart.
/// </summary>
/// <remarks>
/// This response contains details of the newly created cart, including its unique identifier, associated user ID,
/// date of creation, and a list of products within the cart.
/// </remarks>
public class CreateCartResult : GetCartResult
{
}