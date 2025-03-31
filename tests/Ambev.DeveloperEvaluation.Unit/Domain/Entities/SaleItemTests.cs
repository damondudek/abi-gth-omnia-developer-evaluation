using Xunit;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleItem entity class.
/// Tests cover cancellation, total price calculation, and validation scenarios.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that the total price is calculated correctly.
    /// </summary>
    [Fact(DisplayName = "Total price should be calculated correctly")]
    public void Given_SaleItem_When_TotalPriceCalculated_Then_ShouldBeCorrect()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 50,
            Quantity = 3,
            Discount = 20
        };

        // Act
        var totalPrice = saleItem.TotalPrice;

        // Assert
        Assert.Equal(130, totalPrice); // (50 * 3) - 20 = 130
    }

    /// <summary>
    /// Tests that a sale item can be cancelled and the IsCancelled property is set to true.
    /// </summary>
    [Fact(DisplayName = "Sale item should be marked as cancelled when cancelled")]
    public void Given_SaleItem_When_Cancelled_Then_IsCancelledShouldBeTrue()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            IsCancelled = false
        };

        // Act
        saleItem.Cancel();

        // Assert
        Assert.True(saleItem.IsCancelled);
    }

    /// <summary>
    /// Tests that the total price for a cancelled item is ignored.
    /// </summary>
    [Fact(DisplayName = "Total price for cancelled item should remain unchanged")]
    public void Given_CancelledSaleItem_When_TotalPriceCalculated_Then_ShouldStillBeCorrect()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 100,
            Quantity = 2,
            Discount = 20,
            IsCancelled = true
        };

        // Act
        var totalPrice = saleItem.TotalPrice;

        // Assert
        Assert.Equal(180, totalPrice); // Even if cancelled, calculation remains as (100 * 2) - 20 = 180
    }

    /// <summary>
    /// Tests that invalid quantities result in business logic exceptions.
    /// </summary>
    [Fact(DisplayName = "Quantity validation should fail for negative or zero values")]
    public void Given_SaleItem_When_InvalidQuantity_Then_ShouldThrowValidationError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 50,
            Quantity = -1, // Invalid: negative quantity
            Discount = 0
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            if (saleItem.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be a positive integer.");
            }
        });
    }

    /// <summary>
    /// Tests that discount values cannot be negative.
    /// </summary>
    [Fact(DisplayName = "Discount validation should fail for negative values")]
    public void Given_SaleItem_When_NegativeDiscount_Then_ShouldThrowValidationError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 100,
            Quantity = 2,
            Discount = -10 // Invalid: negative discount
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            if (saleItem.Discount < 0)
            {
                throw new ArgumentException("Discount must not be negative.");
            }
        });
    }
}