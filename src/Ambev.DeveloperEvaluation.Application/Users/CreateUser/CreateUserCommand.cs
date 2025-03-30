using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user.
/// </summary>
/// <remarks>
/// This command captures the necessary data for creating a new user, including username, password, 
/// phone number, email, status, and role. It implements <see cref="IRequest{TResponse}"/> to initiate 
/// a request and return a <see cref="CreateUserResult"/>.
///
/// The data in this command is validated using the <see cref="CreateUserCommandValidator"/>, which 
/// extends <see cref="AbstractValidator{T}"/> to ensure all fields adhere to the required rules.
/// </remarks>
public class CreateUserCommand : IRequest<CreateUserResult>
{
    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username. Must be unique and adhere to the required format.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements such as minimum length 
    /// and complexity.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public CreateUserName Name { get; set; } = new CreateUserName();

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public CreateUserAddress Address { get; set; } = new CreateUserAddress();

    /// <summary>
    /// Gets or sets the phone number. Must follow the format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account (e.g., Active, Inactive).
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role assigned to the user (e.g., Admin, User).
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Validates the command using the <see cref="CreateUserCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> object containing:
    /// - <c>IsValid</c>: True if all validations passed.
    /// - <c>Errors</c>: A collection of validation errors, if any exist.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateUserCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the full name of the user.
/// </summary>
public class CreateUserName
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}

/// <summary>
/// Represents the address details of the user.
/// </summary>
public class CreateUserAddress
{
    /// <summary>
    /// Gets or sets the city of the user's address.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street name of the user's address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house or building number of the user's address.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zip code of the user's address.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geolocation details (latitude and longitude) of the user's address.
    /// </summary>
    public CreateUserAddressGeolocation Geolocation { get; set; } = new CreateUserAddressGeolocation();
}

/// <summary>
/// Represents the geolocation coordinates of the user's address.
/// </summary>
public class CreateUserAddressGeolocation
{
    /// <summary>
    /// Gets or sets the latitude coordinate.
    /// </summary>
    public decimal Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate.
    /// </summary>
    public decimal Long { get; set; }
}