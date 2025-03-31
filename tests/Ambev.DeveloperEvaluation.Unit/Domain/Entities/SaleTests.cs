using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover customer updates, adding items, and cancellation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a customer is updated, the new customer information is correctly reflected.
    /// </summary>
    [Fact(DisplayName = "Customer information should be updated correctly")]
    public void Given_Sale_When_CustomerInfoUpdated_Then_CustomerDetailsShouldBeCorrect()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            CustomerName = "Old Customer"
        };
        var newCustomerId = Guid.NewGuid();
        var newCustomerName = "New Customer";

        // Act
        sale.UpdateCustomerInfo(newCustomerId, newCustomerName);

        // Assert
        Assert.Equal(newCustomerId, sale.CustomerId);
        Assert.Equal(newCustomerName, sale.CustomerName);
        Assert.Contains(sale.DomainEvents, e => e is SaleModifiedEvent);
    }

    /// <summary>
    /// Tests that a new item can be added to the sale and the total amount is updated correctly.
    /// </summary>
    [Fact(DisplayName = "Adding an item should update total amount")]
    public void Given_Sale_When_ItemAdded_Then_TotalAmountShouldBeUpdated()
    {
        // Arrange
        var sale = new Sale();
        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductName = "Product A",
            Quantity = 2,
            UnitPrice = 50
        };

        // Act
        sale.AddItem(item);

        // Assert
        Assert.Single(sale.Items);
        Assert.Equal(100, sale.TotalAmount);
        Assert.Contains(sale.DomainEvents, e => e is SaleModifiedEvent);
    }

    /// <summary>
    /// Tests that a sale can be canceled and the IsCancelled property is set to true.
    /// </summary>
    [Fact(DisplayName = "Sale should be marked as cancelled when cancelled")]
    public void Given_Sale_When_Cancelled_Then_IsCancelledShouldBeTrue()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Cancel();

        // Assert
        Assert.True(sale.IsCancelled);
        Assert.Contains(sale.DomainEvents, e => e is SaleCancelledEvent);
    }

    /// <summary>
    /// Tests that a specific item in the sale can be cancelled and the total amount is recalculated.
    /// </summary>
    [Fact(DisplayName = "Item cancellation should update total amount")]
    public void Given_SaleWithItems_When_ItemCancelled_Then_TotalAmountShouldBeRecalculated()
    {
        // Arrange
        var sale = new Sale();
        var item1 = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductName = "Product A",
            Quantity = 1,
            UnitPrice = 100
        };
        var item2 = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductName = "Product B",
            Quantity = 2,
            UnitPrice = 50
        };

        sale.AddItem(item1);
        sale.AddItem(item2);

        // Act
        sale.CancelItem(item2.Id);

        // Assert
        Assert.Equal(100, sale.TotalAmount); // Only item1 remains
        Assert.Single(sale.Items.Where(i => !i.IsCancelled));
        Assert.Contains(sale.DomainEvents, e => e is ItemCancelledEvent);
    }

    /// <summary>
    /// Tests that when a new sale is created, the total amount starts at zero.
    /// </summary>
    [Fact(DisplayName = "New sale should have zero total amount")]
    public void Given_NewSale_When_Created_Then_TotalAmountShouldBeZero()
    {
        // Arrange & Act
        var sale = new Sale();

        // Assert
        Assert.Equal(0, sale.TotalAmount);
    }
}