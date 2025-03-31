namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer in the system, including their personal details such as first and last name.
/// This entity follows domain-driven design principles and serves as a reference for sales and other operations involving customers.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}