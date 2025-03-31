namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// A generic repository interface that provides common CRUD operations for all entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity the repository handles</typeparam>
public interface IBaseReadOnlyRepository<TEntity> where TEntity : class
{

    /// <summary>
    /// Retrieve an entity by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve an entity by ids
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
