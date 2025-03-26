using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartProductRepository for CartProduct-specific operations
/// </summary>
public class CartProductRepository : BaseRepository<CartProduct, DefaultContext>, ICartProductRepository
{
    public CartProductRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves all cart products from the database
    /// </summary>
    public Task<List<CartProduct>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.CartProducts.AsNoTracking().ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves cart products by the cart ID
    /// </summary>
    public Task<List<CartProduct>> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        => _context.CartProducts.AsNoTracking().Where(cp => cp.CartId == cartId).ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves a specific cart product by its product ID and cart ID
    /// </summary>
    public Task<CartProduct?> GetByProductIdAndCartIdAsync(Guid productId, Guid cartId, CancellationToken cancellationToken = default)
        => _context.CartProducts.AsNoTracking().FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.CartId == cartId, cancellationToken);
}