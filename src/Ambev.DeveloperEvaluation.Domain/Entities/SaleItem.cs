using Ambev.DeveloperEvaluation.Domain.Common;

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
    public Guid SaleId { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the product associated with this sale item.
    /// This acts as an external identity referencing the Product domain.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Gets the name of the product associated with this sale item.
    /// Provides a denormalized snapshot for quick access.
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// Gets the unit price of the product in this sale item.
    /// The price must be a positive number.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Gets the quantity of the product in this sale item.
    /// The quantity must be a positive integer.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the discount applied to the sale item.
    /// The discount must not be negative.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the sale item is cancelled.
    /// If true, the item is no longer part of the active sale.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Gets the total price for the sale item, calculated as (UnitPrice * Quantity) - Discount.
    /// Excludes cancelled items from sale calculations.
    /// </summary>
    public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItem"/> class with the provided details.
    /// Validates that the unit price, quantity, and discount meet business rules.
    /// </summary>
    /// <param name="saleId">The identifier of the sale this item belongs to.</param>
    /// <param name="productId">The identifier of the product associated with the sale item.</param>
    /// <param name="productName">The name of the product.</param>
    /// <param name="unitPrice">The unit price of the product. Must be positive.</param>
    /// <param name="quantity">The quantity of the product. Must be positive.</param>
    /// <param name="discount">The discount applied to the item. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown if unit price, quantity, or discount are invalid.</exception>
    public SaleItem(
        Guid saleId,
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity,
        decimal discount)
    {
        SaleId = saleId;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice > 0 ? unitPrice : throw new ArgumentException("Price must be positive");
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be positive");
        Discount = discount >= 0 ? discount : throw new ArgumentException("Discount cannot be negative");
    }

    /// <summary>
    /// Cancels the sale item. Sets the <see cref="IsCancelled"/> property to true.
    /// </summary>
    public void Cancel() => IsCancelled = true;
}