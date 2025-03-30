using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for the <see cref="CreateUserCommand"/> class.
/// Defines comprehensive validation rules for user creation commands.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class,
    /// defining validation rules for the CreateUserCommand.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item><description>Email: Must be valid format and non-empty.</description></item>
    /// <item><description>Username: Required, must be between 3 and 50 characters, and contain only alphanumeric or underscore characters.</description></item>
    /// <item><description>Password: Must meet complexity requirements (minimum length 8).</description></item>
    /// <item><description>Phone: Must match international format (+X XXXXXXXXXX).</description></item>
    /// <item><description>Status: Must not be set to <see cref="UserStatus.Unknown"/>.</description></item>
    /// <item><description>Role: Must not be set to <see cref="UserRole.None"/>.</description></item>
    /// <item><description>Name: Firstname and Lastname cannot be empty.</description></item>
    /// <item><description>Address: City, Street, Number, and ZipCode must be provided; Geolocation must contain valid latitude and longitude.</description></item>
    /// </list>
    /// </remarks>
    public CreateUserCommandValidator()
    {
        // Email validation
        RuleFor(user => user.Email)
            .SetValidator(new EmailValidator());

        // Username validation
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Username can only contain alphanumeric characters and underscores.");

        // Password validation
        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator());

        // Phone validation
        RuleFor(user => user.Phone)
            .SetValidator(new PhoneValidator());

        // Status validation
        RuleFor(user => user.Status)
            .IsInEnum().WithMessage("Invalid status value.")
            .NotEqual(UserStatus.Unknown).WithMessage("Status cannot be Unknown.");

        // Role validation
        RuleFor(user => user.Role)
            .IsInEnum().WithMessage("Invalid role value.")
            .NotEqual(UserRole.None).WithMessage("Role cannot be None.");

        // Name validation (FirstName and LastName)
        RuleFor(user => user.Name.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Length(1, 50).WithMessage("FirstName must be between 1 and 50 characters.");
        RuleFor(user => user.Name.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .Length(1, 50).WithMessage("LastName must be between 1 and 50 characters.");

        // Address validation (City, Street, Number, ZipCode)
        RuleFor(user => user.Address.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(1, 100).WithMessage("City name must be between 1 and 100 characters.");
        RuleFor(user => user.Address.Street)
            .NotEmpty().WithMessage("Street is required.")
            .Length(1, 100).WithMessage("Street name must be between 1 and 100 characters.");
        RuleFor(user => user.Address.Number)
            .NotEmpty().WithMessage("Number is required.")
            .Matches(@"^\d+[a-zA-Z]?$").WithMessage("Number must be numeric and may contain an optional suffix.");
        RuleFor(user => user.Address.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .Matches(@"^\d{8}$").WithMessage("The zip code format is not valid.");

        // Geolocation validation (Latitude and Longitude)
        RuleFor(user => user.Address.Geolocation.Lat)
            .NotNull().WithMessage("Latitude must be provided.");
        RuleFor(user => user.Address.Geolocation.Long)
            .NotNull().WithMessage("Longitude must be provided.");
    }
}