using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateProductCommand entities.
    /// The generated products will have:
    /// - Title (random product names)
    /// - Price (randomized within a valid range)
    /// - Description (random descriptions)
    /// - Category (random category names)
    /// - Image (random URLs simulating product images)
    /// - Rating (nested object with random values)
    /// </summary>
    private static readonly Faker<CreateProductCommand> productHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Finance.Amount(1, 500)) // Random price between 1 and 500
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First()) // Random category
        .RuleFor(p => p.Image, f => f.Internet.UrlWithPath()) // Simulating image URLs
        .RuleFor(p => p.Rating, f => GenerateRandomRating()); // Generate random rating details

    /// <summary>
    /// Generates valid CreateProductCommand entities with randomized data.
    /// The generated products will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateProductCommand entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return productHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a list of valid CreateProductCommand entities for bulk testing.
    /// </summary>
    /// <param name="count">The number of product commands to generate.</param>
    /// <returns>A list of valid CreateProductCommand entities with randomized data.</returns>
    public static IEnumerable<CreateProductCommand> GenerateValidCommands(int count)
    {
        return productHandlerFaker.Generate(count);
    }

    /// <summary>
    /// Configures the Faker to generate valid CreateProductRatingCommand objects.
    /// </summary>
    /// <returns>A CreateProductRatingCommand with random values for Rate and Count.</returns>
    private static CreateProductRatingCommand GenerateRandomRating()
    {
        return new Faker<CreateProductRatingCommand>()
            .RuleFor(r => r.Rate, f => f.Random.Double(0, 5)) // Random rating between 0 and 5
            .RuleFor(r => r.Count, f => f.Random.Int(0, 1000)) // Random number of ratings
            .Generate();
    }
}