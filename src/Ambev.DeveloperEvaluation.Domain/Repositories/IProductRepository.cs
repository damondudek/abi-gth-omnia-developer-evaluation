using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{

    /// <summary>
    /// Retrieves products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products if found, null otherwise</returns>
    Task<PaginatedList<Product>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its title
    /// </summary>
    /// <param name="title">The title to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves product`s categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product`s categories</returns>
    Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves products by category
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Products by category</returns>
    Task<PaginatedList<Product>> GetPaginatedByCategoryAsync(string category, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
