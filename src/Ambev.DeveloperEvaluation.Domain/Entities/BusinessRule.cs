namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a business configuration rule in the system.
/// </summary>
public class BusinessRule
{
    /// <summary>
    /// Gets or sets the key identifying the discount configuration rule (e.g., MinQuantityForDiscount).
    /// </summary>
    public string ConfigKey { get; set; }

    /// <summary>
    /// Gets or sets the value of the discount configuration rule.
    /// </summary>
    public string ConfigValue { get; set; }
}