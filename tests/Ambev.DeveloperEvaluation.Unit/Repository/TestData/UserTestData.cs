using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Repository.TestData;

/// <summary>
/// Provides methods for generating test User data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UserTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have:
    /// - Id (valid GUID)
    /// - Username (valid username format)
    /// - Email (valid email format)
    /// - Status (random valid UserStatus)
    /// - CreatedAt (recent date)
    /// - Optional IsAdmin flag
    /// </summary>
    private static readonly Faker<User> _userFaker = new Faker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
        .RuleFor(u => u.CreatedAt, f => f.Date.Recent(30));

    /// <summary>
    /// Generates a single valid User entity with randomized data.
    /// </summary>
    /// <returns>A valid User with randomly generated data.</returns>
    public static User GenerateValidUser()
    {
        return _userFaker.Generate();
    }

    /// <summary>
    /// Generates a list of valid User entities with randomized data.
    /// </summary>
    /// <param name="count">Number of users to generate (default: 4)</param>
    /// <returns>List of valid Users with randomly generated data.</returns>
    public static List<User> GenerateValidUsers(int count = 4)
    {
        return _userFaker.Generate(count);
    }

    /// <summary>
    /// Generates a list of test users with specific predictable values.
    /// Useful for tests that need consistent data.
    /// </summary>
    /// <returns>List of test users with predefined values.</returns>
    public static List<User> GenerateTestUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Email = "user1@example.com",
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2023, 1, 1)
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "user2",
                Email = "user2@example.com",
                Status = UserStatus.Inactive,
                CreatedAt = new DateTime(2023, 1, 2)
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin1",
                Email = "admin1@example.com",
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2023, 1, 3)
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin2",
                Email = "admin2@example.com",
                Status = UserStatus.Suspended,
                CreatedAt = new DateTime(2023, 1, 4)
            }
        };
    }

    /// <summary>
    /// Generates a user with invalid data for testing validation.
    /// </summary>
    /// <returns>A User with intentionally invalid data.</returns>
    public static User GenerateInvalidUser()
    {
        return new User
        {
            Id = Guid.Empty,
            Username = "", // Invalid empty username
            Email = "not-an-email", // Invalid email format
            Status = (UserStatus)999, // Invalid status value
            CreatedAt = DateTime.MinValue // Unrealistic date
        };
    }

    /// <summary>
    /// Generates a user with specific status for testing status-related logic.
    /// </summary>
    /// <param name="status">The desired UserStatus</param>
    /// <returns>A User with the specified status.</returns>
    public static User GenerateUserWithStatus(UserStatus status)
    {
        var user = _userFaker.Generate();
        user.Status = status;
        return user;
    }
}