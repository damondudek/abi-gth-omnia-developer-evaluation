using Ambev.DeveloperEvaluation.Domain.Entities;

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
    /// <returns>The products</returns>
    Task<List<Product>> GetByPaginationAsync(CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves product`s categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product`s categories</returns>
    Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken = default);
}
