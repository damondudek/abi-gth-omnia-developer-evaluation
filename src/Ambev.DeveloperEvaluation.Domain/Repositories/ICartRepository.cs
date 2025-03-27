using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository : IBaseRepository<Cart>
{
    /// <summary>
    /// Retrieve all carts from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of carts</returns>
    Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves paginated carts
    /// </summary>
    /// <param name="pageNumber">Page number to retrieve</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of carts</returns>
    Task<PaginatedList<Cart>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves carts by the user ID
    /// </summary>
    /// <param name="userId">The user ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart`s list</returns>
    Task<List<Cart>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}