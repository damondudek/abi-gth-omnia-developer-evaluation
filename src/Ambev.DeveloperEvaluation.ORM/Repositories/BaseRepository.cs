using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// A generic repository providing common data access functionality for all entities.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TContext">The DbContext type</typeparam>
public class BaseRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of BaseRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BaseRepository(TContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Creates a new entity in the database
    /// </summary>
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
            baseEntity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Updates an existing entity in the database
    /// </summary>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.UpdatedAt = DateTime.UtcNow;

            var originalEntity = await GetByIdAsync(baseEntity.Id, cancellationToken);

            if (originalEntity != null)
            {
                if (originalEntity is BaseEntity originalBaseEntity)
                {
                    baseEntity.CreatedAt = originalBaseEntity.CreatedAt;
                }
            }
        }

        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    public virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);


    public virtual Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => _dbSet.AsNoTracking().Where(p => ids.Contains(EF.Property<Guid>(p, "Id"))).ToListAsync(cancellationToken);

    /// <summary>
    /// Deletes an entity from the database
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
