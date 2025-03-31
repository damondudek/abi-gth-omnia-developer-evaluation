namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale, including product details, pricing, and cancellation status.
/// This entity follows domain-driven design principles, encapsulating business rules for individual sale items.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the sale that this item belongs to.
    /// </summary>
    public Guid SaleId { get; set; }
    public Sale? Sale { get; set; }

    /// <summary>
    /// Gets the unique identifier of the product associated with this sale item.
    /// This acts as an external identity referencing the Product domain.
    /// </summary>
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    /// <summary>
    /// Gets the name of the product associated with this sale item.
    /// Provides a denormalized snapshot for quick access.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the unit price of the product in this sale item.
    /// The price must be a positive number.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the quantity of the product in this sale item.
    /// The quantity must be a positive integer.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the discount applied to the sale item.
    /// The discount must not be negative.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets a value indicating whether the sale item is cancelled.
    /// If true, the item is no longer part of the active sale.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the total price for the sale item, calculated as (UnitPrice * Quantity) - Discount.
    /// Excludes cancelled items from sale calculations.
    /// </summary>
    public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

    /// <summary>
    /// Cancels the sale item. Sets the <see cref="IsCancelled"/> property to true.
    /// </summary>
    public void Cancel() => IsCancelled = true;
}