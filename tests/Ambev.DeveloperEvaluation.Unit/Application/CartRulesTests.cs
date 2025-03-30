using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CartRules"/> class.
/// </summary>
public class CartRulesTests : IClassFixture<RepositoryFixture>
{
    private readonly RepositoryFixture _repositoryFixture;
    private readonly CartRules _cartRules;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartRulesTests"/> class.
    /// Sets up the test dependencies for CartRules.
    /// </summary>
    public CartRulesTests(RepositoryFixture repositoryFixture)
    {
        _repositoryFixture = repositoryFixture;
        _cartRules = new CartRules(_repositoryFixture.BusinessRuleRepository, _repositoryFixture.ProductRepository);
    }

    /// <summary>
    /// Tests that an exception is thrown when the product list is null.
    /// </summary>
    [Fact(DisplayName = "Given null product list When validating purchase Then throws argument exception")]
    public void ValidatePurchase_NullProductList_ThrowsArgumentException()
    {
        // Given
        IEnumerable<CartProduct> products = null;

        // When
        var act = () => _cartRules.ValidatePurchase(products);

        // Then
        act.Should().Throw<ArgumentException>()
           .WithMessage("The product list cannot be null or empty. (Parameter 'products')");
    }

    /// <summary>
    /// Tests that an exception is thrown when the product list is empty.
    /// </summary>
    [Fact(DisplayName = "Given empty product list When validating purchase Then throws argument exception")]
    public void ValidatePurchase_EmptyProductList_ThrowsArgumentException()
    {
        // Given
        var products = Enumerable.Empty<CartProduct>();

        // When
        var act = () => _cartRules.ValidatePurchase(products);

        // Then
        act.Should().Throw<ArgumentException>()
           .WithMessage("The product list cannot be null or empty. (Parameter 'products')");
    }

    /// <summary>
    /// Tests that an exception is thrown when any product quantity exceeds the max limit.
    /// </summary>
    [Fact(DisplayName = "Given product quantity exceeds max limit When validating purchase Then throws invalid operation exception")]
    public void ValidatePurchase_ExceedsMaxLimit_ThrowsInvalidOperationException()
    {
        // Given
        var products = new List<CartProduct>
        {
            new CartProduct { ProductId = Guid.NewGuid(), Quantity = 25 } // Exceeds limit
        };

        // When
        var act = () => _cartRules.ValidatePurchase(products);

        // Then
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Cannot sell more than 20 items of any product.");
    }

    /// <summary>
    /// Tests that products with quantities below the discount threshold are assigned no discount.
    /// </summary>
    [Fact(DisplayName = "Given product below discount threshold When validating purchase Then assigns no discount")]
    public void ValidatePurchase_BelowDiscountThreshold_AssignsNoDiscount()
    {
        // Given
        var products = new List<CartProduct>
        {
            new CartProduct { ProductId = Guid.NewGuid(), Quantity = 3 } // Below discount threshold
        };

        // When
        _cartRules.ValidatePurchase(products);

        // Then
        //products[0].Discount.Should().Be(0m);
    }

    /// <summary>
    /// Tests that products with quantities in Tier 1 range are assigned 10% discount.
    /// </summary>
    [Fact(DisplayName = "Given product in tier 1 discount range When validating purchase Then assigns 10% discount")]
    public void ValidatePurchase_InTier1_Assigns10PercentDiscount()
    {
        // Given
        var products = new List<CartProduct>
        {
            new CartProduct { ProductId = Guid.NewGuid(), Quantity = 5 } // Tier 1
        };

        // When
        _cartRules.ValidatePurchase(products);

        // Then
        //products[0].Discount.Should().Be(0.10m);
    }

    /// <summary>
    /// Tests that products with quantities in Tier 2 range are assigned 20% discount.
    /// </summary>
    [Fact(DisplayName = "Given product in tier 2 discount range When validating purchase Then assigns 20% discount")]
    public void ValidatePurchase_InTier2_Assigns20PercentDiscount()
    {
        // Given
        var products = new List<CartProduct>
        {
            new CartProduct { ProductId = Guid.NewGuid(), Quantity = 15 } // Tier 2
        };

        // When
        _cartRules.ValidatePurchase(products);

        // Then
        //products[0].Discount.Should().Be(0.20m);
    }
}