using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Validator for the <see cref="CreateUserRequest"/> class.
/// Defines rules to ensure data validity for user creation requests.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserRequestValidator"/> class,
    /// defining comprehensive validation rules for the CreateUserRequest.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item><description>Email: Must be in a valid format and non-empty.</description></item>
    /// <item><description>Username: Required, must be between 3 and 50 characters, and alphanumeric.</description></item>
    /// <item><description>Password: Must meet security requirements, including length and complexity.</description></item>
    /// <item><description>Phone: Must match international format (+X XXXXXXXXXX).</description></item>
    /// <item><description>Name: Both FirstName and LastName must be non-empty and within length limits.</description></item>
    /// <item><description>Address: All fields must be properly formatted and non-null, including geolocation.</description></item>
    /// <item><description>Status: Cannot be set to <see cref="UserStatus.Unknown"/>.</description></item>
    /// <item><description>Role: Cannot be set to <see cref="UserRole.None"/>.</description></item>
    /// </list>
    /// </remarks>
    public CreateUserRequestValidator()
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

        // Name validation (FirstName and LastName)
        RuleFor(user => user.Name)
            .NotNull().WithMessage("Name cannot be null.")
            .ChildRules(name =>
            {
                name.RuleFor(n => n.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.")
                    .Length(3, 50).WithMessage("FirstName must be between 3 and 50 characters.");
                name.RuleFor(n => n.LastName)
                    .NotEmpty().WithMessage("LastName is required.")
                    .Length(3, 50).WithMessage("LastName must be between 3 and 50 characters.");
            });

        // Address validation (City, Street, Number, ZipCode)
        RuleFor(user => user.Address)
            .NotNull().WithMessage("Address cannot be null.")
            .ChildRules(address =>
            {
                address.RuleFor(a => a.City)
                    .NotEmpty().WithMessage("City is required.")
                    .Length(1, 100).WithMessage("City name must be between 1 and 100 characters.");
                address.RuleFor(a => a.Street)
                    .NotEmpty().WithMessage("Street is required.")
                    .Length(1, 100).WithMessage("Street name must be between 1 and 100 characters.");
                address.RuleFor(a => a.Number)
                    .NotEmpty().WithMessage("Number is required.");
                address.RuleFor(a => a.ZipCode)
                    .NotEmpty().WithMessage("Zip code is required.");
                address.RuleFor(a => a.Geolocation)
                    .NotNull().WithMessage("Geolocation must be provided.")
                    .ChildRules(geo =>
                    {
                        geo.RuleFor(g => g.Lat)
                            .NotNull().WithMessage("Latitude must be provided.");
                        geo.RuleFor(g => g.Long)
                            .NotNull().WithMessage("Longitude must be provided.");
                    });
            });

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
    }
}