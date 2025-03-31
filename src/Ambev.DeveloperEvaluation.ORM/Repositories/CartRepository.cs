using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository for Cart-specific operations
/// </summary>
public class CartRepository : BaseRepository<Cart, DefaultContext>, ICartRepository
{
    public CartRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    public override Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Carts.Where(c => c.Id == id).AsNoTracking().Include(c => c.Products).FirstOrDefaultAsync(cancellationToken);

    /// <summary>
    /// Updates an existing cart in the database
    /// </summary>
    public override async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var existingItems = await _context.CartProducts
            .Where(cp => cp.CartId == cart.Id)
            .ToListAsync(cancellationToken);

        var itemsToRemove = existingItems
            .Where(ei => !cart.Products.Any(cp => cp.Id == ei.Id))
            .ToList();

        if (itemsToRemove.Count != 0)
            _context.CartProducts.RemoveRange(itemsToRemove);

        cart.UpdatedAt = DateTime.UtcNow;
        cart.SetProductsUpdatedAt();

        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);

        return cart;
    }

    public Task<PaginatedList<Cart>> GetPaginatedAsync(int pageNumber, int pageSize, string orderBy, Dictionary<string, string> filters, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts.AsNoTracking().Include(c => c.Products).AsQueryable();
        var pagedList = query.ToPagedListAsync(pageNumber, pageSize, orderBy, filters, cancellationToken);

        return pagedList;
    }

    /// <summary>
    /// Retrieves cart`s list by its user ID
    /// </summary>
    public Task<List<Cart>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => _context.Carts.AsNoTracking().Include(c => c.Products).Where(c => c.UserId == userId).ToListAsync(cancellationToken);
}