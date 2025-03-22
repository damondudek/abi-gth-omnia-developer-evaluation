using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{
    /// <summary>
    /// Retrieves users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<List<Product>> GetByPaginationAsync(CancellationToken cancellationToken = default);
}
