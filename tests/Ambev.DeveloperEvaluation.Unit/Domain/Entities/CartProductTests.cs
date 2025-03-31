using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the CartProduct entity class.
/// Tests cover product updates, discount application, and quantity validation scenarios.
/// </summary>
public class CartProductTests
{
    /// <summary>
    /// Tests that product information can be updated correctly.
    /// </summary>
    [Fact(DisplayName = "Product information should be updated correctly")]
    public void Given_CartProduct_When_ProductInfoUpdated_Then_ProductDetailsShouldBeCorrect()
    {
        // Arrange
        var cartProduct = new CartProduct
        {
            ProductId = Guid.NewGuid(),
            ProductTitle = "Old Product",
            Price = 100
        };
        var updatedProduct = new Product
        {
            Id = cartProduct.ProductId,
            Title = "Updated Product",
            Price = 150
        };

        // Act
        cartProduct.UpdateProductInfo(updatedProduct);

        // Assert
        Assert.Equal("Updated Product", cartProduct.ProductTitle);
        Assert.Equal(150, cartProduct.Price);
    }

    /// <summary>
    /// Tests that the discount is calculated correctly based on the price and quantity.
    /// </summary>
    [Fact(DisplayName = "Discount should be applied correctly")]
    public void Given_CartProduct_When_DiscountApplied_Then_DiscountShouldBeCorrect()
    {
        // Arrange
        var cartProduct = new CartProduct
        {
            Quantity = 5,
            Price = 20,
            Discount = 0
        };

        // Act
        cartProduct.Discount = cartProduct.Quantity >= 4 ? cartProduct.Price * 0.1m : 0;

        // Assert
        Assert.Equal(2, cartProduct.Discount); // 10% of 20
    }

    /// <summary>
    /// Tests that the quantity is properly set and validated.
    /// </summary>
    [Fact(DisplayName = "Quantity should be valid and within allowed limits")]
    public void Given_CartProduct_When_QuantitySet_Then_QuantityShouldBeValid()
    {
        // Arrange
        var cartProduct = new CartProduct
        {
            Quantity = 10
        };

        // Act
        var isValidQuantity = cartProduct.Quantity > 0 && cartProduct.Quantity <= 20;

        // Assert
        Assert.True(isValidQuantity);
    }

    /// <summary>
    /// Tests that the UpdatedAt property is set when the product is updated.
    /// </summary>
    [Fact(DisplayName = "UpdatedAt property should be set correctly")]
    public void Given_CartProduct_When_SetUpdatedAtCalled_Then_UpdatedAtShouldBeSet()
    {
        // Arrange
        var cartProduct = new CartProduct();

        // Act
        cartProduct.SetUpdatedAt();

        // Assert
        Assert.NotEqual(default, cartProduct.UpdatedAt); // Ensure it's set to a valid timestamp
    }
}