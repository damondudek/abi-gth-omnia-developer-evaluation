using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductCategoryRepository using Entity Framework Core
/// </summary>
public class ProductCategoryRepository : BaseRepository<ProductCategory, DefaultContext>, IProductCategoryRepository
{
    /// <summary>
    /// Initializes a new instance of ProductCategoryRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductCategoryRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves all entities from the database
    /// </summary>
    public Task<List<ProductCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.ProductCategories.AsNoTracking().ToListAsync(cancellationToken);
}
