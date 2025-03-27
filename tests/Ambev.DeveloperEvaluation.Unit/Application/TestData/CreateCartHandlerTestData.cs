using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated carts will have:
    /// - UserId (valid GUID)
    /// - Products (valid product entries with product ID and quantity)
    /// </summary>
    private static readonly Faker<CreateCartCommand> createCartHandlerFaker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.UserId, f => Guid.NewGuid()) // Random GUID for UserId
        .RuleFor(c => c.Products, f => GenerateRandomProducts(f.Random.Number(1, 5))); // 1 to 5 random products

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static CreateCartCommand GenerateValidCommand()
    {
        return createCartHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a list of random products for test carts.
    /// Each product will have:
    /// - ProductId (valid GUID)
    /// - Quantity (random positive integer)
    /// </summary>
    /// <param name="productCount">The number of products to generate.</param>
    /// <returns>A list of randomly generated products.</returns>
    private static List<CreateCartProductCommand> GenerateRandomProducts(int productCount)
    {
        var productFaker = new Faker<CreateCartProductCommand>()
            .RuleFor(p => p.ProductId, f => Guid.NewGuid()) // Random GUID for ProductId
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 10)); // Random quantity between 1 and 10

        return productFaker.Generate(productCount);
    }
}