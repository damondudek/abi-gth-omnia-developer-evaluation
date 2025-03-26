namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

/// <summary>
/// Request model for getting products by category
/// </summary>
public class GetProductsByCategoryRequest
{
    /// <summary>
    /// The category identifier to retrieve products
    /// </summary>
    public string Category { get; set; }
}