using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

/// <summary>
/// Command for retrieving products by their category
/// </summary>
public class GetProductsByCategoryCommand : PaginatedCommand<GetProductResult>
{
    /// <summary>
    /// The category of the products to retrieve
    /// </summary>
    public string Category { get; set; }
}