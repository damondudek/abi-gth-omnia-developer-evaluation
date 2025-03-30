using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a user entity in the system with authentication, profile, and business logic validation.
/// This entity follows domain-driven design principles.
/// </summary>
public class User : BaseEntity, IUser
{
    /// <summary>
    /// Gets or sets the collection of carts associated with the user.
    /// </summary>
    public ICollection<Cart> Carts { get; set; } = Array.Empty<Cart>();

    /// <summary>
    /// Gets or sets the user's username for login purposes.
    /// Must not be null or empty.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's first name.
    /// Must not be null or empty.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// Must not be null or empty.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// Must be a valid email format and serves as a unique identifier for authentication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's phone number in the format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password for authentication.
    /// Must meet security requirements: minimum 8 characters, one uppercase letter, 
    /// one lowercase letter, one number, and one special character.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role in the system.
    /// Defines permissions and access levels.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the user's current account status.
    /// Indicates whether the user is active, inactive, or suspended.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the city of the user's address.
    /// </summary>
    public string AddressCity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street name of the user's address.
    /// </summary>
    public string AddressStreet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house or building number of the user's address.
    /// </summary>
    public string AddressNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zip code of the user's address.
    /// </summary>
    public string AddressZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the latitude coordinate of the user's address.
    /// </summary>
    public decimal AddressLat { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the user's address.
    /// </summary>
    public decimal AddressLong { get; set; }

    /// <summary>
    /// Gets the user's unique identifier as a string.
    /// </summary>
    string IUser.Id => Id.ToString();

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    string IUser.Username => Username;

    /// <summary>
    /// Gets the user's role in the system as a string.
    /// </summary>
    string IUser.Role => Role.ToString();

    /// <summary>
    /// Validates the user entity using predefined rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing the validation outcome:
    /// - <c>IsValid</c>: Indicates whether all rules passed.
    /// - <c>Errors</c>: Contains validation errors if any rules failed.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UserValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
        };
    }

    /// <summary>
    /// Activates the user account by changing the status to Active.
    /// Updates the <see cref="UpdatedAt"/> field to the current UTC time.
    /// </summary>
    public void Activate()
    {
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the user account by changing the status to Inactive.
    /// Updates the <see cref="UpdatedAt"/> field to the current UTC time.
    /// </summary>
    public void Deactivate()
    {
        Status = UserStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Suspends the user account by changing the status to Suspended.
    /// Updates the <see cref="UpdatedAt"/> field to the current UTC time.
    /// </summary>
    public void Suspend()
    {
        Status = UserStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }
}