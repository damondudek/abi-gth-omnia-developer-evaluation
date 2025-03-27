using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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
    /// Retrieves all carts from the database
    /// </summary>
    public Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.Carts.AsNoTracking().Include(c => c.Products).ToListAsync(cancellationToken);

    public Task<PaginatedList<Cart>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts.AsNoTracking().Include(c => c.Products).AsQueryable();
        var pagedList = PaginatedList<Cart>.CreateAsync(query, pageNumber, pageSize, cancellationToken);

        return pagedList;
    }

    /// <summary>
    /// Retrieves cart`s list by its user ID
    /// </summary>
    public Task<List<Cart>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => _context.Carts.AsNoTracking().Include(c => c.Products).Where(c => c.UserId == userId).ToListAsync(cancellationToken);
}