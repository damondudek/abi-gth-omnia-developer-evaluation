using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository : IBaseRepository<Sale>
{
    /// <summary>
    /// Retrieves all sales from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales with their items</returns>
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by branch ID
    /// </summary>
    /// <param name="branchId">The branch ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales from the specified branch</returns>
    Task<List<Sale>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by customer ID
    /// </summary>
    /// <param name="customerId">The customer ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales for the specified customer</returns>
    Task<List<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique sale number
    /// </summary>
    /// <param name="saleNumber">The sale number to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales within a specific date range
    /// </summary>
    /// <param name="startDate">The start date of the range</param>
    /// <param name="endDate">The end date of the range</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales within the date range</returns>
    Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves active (non-cancelled) sales
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active sales</returns>
    Task<List<Sale>> GetActiveSalesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves cancelled sales
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of cancelled sales</returns>
    Task<List<Sale>> GetCancelledSalesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales with items that match the specified product ID
    /// </summary>
    /// <param name="productId">The product ID to search for in sale items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales containing the specified product</returns>
    Task<List<Sale>> GetSalesByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
}