using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Domain.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

/// <summary>
/// Command for retrieving products by their category
/// </summary>
public class GetProductsByCategoryCommand : PaginatedCommand, IRequest<PaginatedResponse<GetProductResult>>
{
    /// <summary>
    /// The category of the products to retrieve
    /// </summary>
    public string Category { get; set; }
}