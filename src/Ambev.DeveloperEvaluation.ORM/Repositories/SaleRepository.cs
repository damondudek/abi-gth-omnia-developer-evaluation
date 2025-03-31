using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository for Sale-specific operations
/// </summary>
public class SaleRepository : BaseRepository<Sale, DefaultContext>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves sales by branch ID
    /// </summary>
    public Task<List<Sale>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => s.BranchId == branchId)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves sales by customer ID
    /// </summary>
    public Task<List<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => s.CustomerId == customerId)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves a sale by its number
    /// </summary>
    public Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);

    /// <summary>
    /// Retrieves sales within a date range
    /// </summary>
    public Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves active (non-cancelled) sales
    /// </summary>
    public Task<List<Sale>> GetActiveSalesAsync(CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => !s.IsCancelled)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves cancelled sales
    /// </summary>
    public Task<List<Sale>> GetCancelledSalesAsync(CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => s.IsCancelled)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves sales by product id
    /// </summary>
    public Task<List<Sale>> GetSalesByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        => _context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .Where(s => s.Items.Any(i => i.ProductId == productId))
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    public override async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingItems = await _context.SaleItems
            .Where(si => si.SaleId == sale.Id)
            .ToListAsync(cancellationToken);

        var itemsToRemove = existingItems
            .Where(ei => !sale.Items.Any(si => si.Id == ei.Id))
            .ToList();

        if (itemsToRemove.Count != 0)
            _context.SaleItems.RemoveRange(itemsToRemove);

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }
}