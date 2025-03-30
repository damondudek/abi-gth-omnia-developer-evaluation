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
    /// Retrieves all carts from the database
    /// </summary>
    public Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.Carts.AsNoTracking().Include(c => c.Products).ToListAsync(cancellationToken);

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