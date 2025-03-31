using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateUserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateUserCommand entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active, Inactive, or Suspended)
    /// - Role (Customer, Manager, or Admin)
    /// - Name (firstname and lastname)
    /// - Address (including geolocation)
    /// </summary>
    private static readonly Faker<CreateUserCommand> createUserHandlerFaker = new Faker<CreateUserCommand>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Secure@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Name, f => new CreateUserName
        {
            FirstName = f.Name.FirstName(),
            LastName = f.Name.LastName()
        })
        .RuleFor(u => u.Address, f => new CreateUserAddress
        {
            City = f.Address.City(),
            Street = f.Address.StreetName(),
            Number = f.Random.String(1, 10000),
            ZipCode = f.Address.ZipCode(),
            Geolocation = new CreateUserAddressGeolocation
            {
                Lat = (decimal)f.Address.Latitude(),
                Long = (decimal)f.Address.Longitude()
            }
        });

    /// <summary>
    /// Generates a valid CreateUserCommand entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateUserCommand entity with randomly generated data.</returns>
    public static CreateUserCommand GenerateValidCommand()
    {
        return createUserHandlerFaker.Generate();
    }
}