using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class ProductRepository : BaseRepository<Product, DefaultContext>, IProductRepository
{
    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products</returns>
    public Task<List<Product>> GetByPaginationAsync(CancellationToken cancellationToken = default)
        => _context.Products.AsNoTracking().ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves product`s categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of product`s categories</returns>
    public Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken = default)
        => _context.Products.AsNoTracking().Select(e => e.Category).Distinct().ToListAsync(cancellationToken);


    public Task<PaginatedList<Product>> GetPaginatedByCategoryAsync(string category, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsNoTracking()
            .Where(x => x.Category == category)
            .AsQueryable();

        var pagedList = PaginatedList<Product>.CreateAsync(query, pageNumber, pageSize, cancellationToken);

        return pagedList;
    }
}
