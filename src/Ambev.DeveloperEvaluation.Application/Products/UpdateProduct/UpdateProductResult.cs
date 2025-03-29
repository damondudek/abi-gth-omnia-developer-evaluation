using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a product.
/// </summary>
/// <remarks>
/// This response contains the details of the updated product,
/// including its identifier, title, price, description, category, image, and rating.
/// </remarks>
public class UpdateProductResult : GetProductResult
{
}