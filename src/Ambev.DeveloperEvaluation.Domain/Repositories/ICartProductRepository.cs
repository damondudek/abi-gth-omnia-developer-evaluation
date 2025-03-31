using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for CartProduct entity operations
/// </summary>
public interface ICartProductRepository : IBaseRepository<CartProduct>, IBaseReadOnlyRepository<CartProduct>
{
    /// <summary>
    /// Retrieves cart products by the cart ID
    /// </summary>
    /// <param name="cartId">The cart ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of cart products</returns>
    Task<List<CartProduct>> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific cart product by its product ID and cart ID
    /// </summary>
    /// <param name="productId">The product ID to search for</param>
    /// <param name="cartId">The cart ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart product if found, null otherwise</returns>
    Task<CartProduct?> GetByProductIdAndCartIdAsync(Guid productId, Guid cartId, CancellationToken cancellationToken = default);
}