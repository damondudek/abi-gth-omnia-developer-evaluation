using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Cart entity class.
/// Tests cover validation, product updates, and timestamp scenarios.
/// </summary>
public class CartTests
{
    /// <summary>
    /// Tests that the `Validate` method correctly identifies a valid cart.
    /// </summary>
    [Fact(DisplayName = "Cart validation should pass for valid cart data")]
    public void Given_ValidCartData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            UserFullName = "John Doe",
            Products = new List<CartProduct>
            {
                new CartProduct
                {
                    ProductId = Guid.NewGuid(),
                    ProductTitle = "Product A",
                    Quantity = 2,
                    Price = 50,
                    Discount = 5
                },
                new CartProduct
                {
                    ProductId = Guid.NewGuid(),
                    ProductTitle = "Product B",
                    Quantity = 3,
                    Price = 100,
                    Discount = 10
                }
            }
        };

        // Act
        var validationResult = cart.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    /// <summary>
    /// Tests that the `Validate` method correctly identifies an invalid cart.
    /// </summary>
    [Fact(DisplayName = "Cart validation should fail for invalid cart data")]
    public void Given_InvalidCartData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var cart = new Cart
        {
            UserId = Guid.Empty, // Invalid UserId
            UserFullName = "", // Invalid: empty name
            Products = new List<CartProduct>
            {
                new CartProduct
                {
                    ProductId = Guid.NewGuid(),
                    ProductTitle = "Product A",
                    Quantity = -1, // Invalid quantity
                    Price = 50,
                    Discount = 5
                }
            }
        };

        // Act
        var validationResult = cart.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.NotEmpty(validationResult.Errors);
    }

    /// <summary>
    /// Tests that the `SetProductsUpdatedAt` method correctly updates timestamps for all products.
    /// </summary>
    [Fact(DisplayName = "Products' UpdatedAt timestamps should be updated correctly")]
    public void Given_CartWithProducts_When_SetProductsUpdatedAtCalled_Then_TimestampsShouldBeUpdated()
    {
        // Arrange
        var cart = new Cart
        {
            Products = new List<CartProduct>
            {
                new CartProduct { ProductId = Guid.NewGuid(), ProductTitle = "Product A" },
                new CartProduct { ProductId = Guid.NewGuid(), ProductTitle = "Product B" }
            }
        };

        // Act
        cart.SetProductsUpdatedAt();

        // Assert
        foreach (var product in cart.Products)
        {
            Assert.NotEqual(default, product.UpdatedAt); // Timestamp should be set
        }
    }

    /// <summary>
    /// Tests that the cart correctly handles empty product lists.
    /// </summary>
    [Fact(DisplayName = "Cart should handle empty product list correctly")]
    public void Given_EmptyCart_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            UserFullName = "John Doe",
            Products = new List<CartProduct>() // Empty product list
        };

        // Act
        var validationResult = cart.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.NotEmpty(validationResult.Errors);
    }
}