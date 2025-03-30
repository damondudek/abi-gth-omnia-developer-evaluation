using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetUserResult
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username. Must be unique and adhere to the required format.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public GetUserNameResult Name { get; set; } = new GetUserNameResult();

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public GetUserAddressResult Address { get; set; } = new GetUserAddressResult();

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; set; }
}


/// <summary>
/// Represents the full name of the user.
/// </summary>
public class GetUserNameResult
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
public class GetUserAddressResult
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
    public GetUserAddressGeolocationResult Geolocation { get; set; } = new GetUserAddressGeolocationResult();
}

/// <summary>
/// Represents the geolocation coordinates of the user's address.
/// </summary>
public class GetUserAddressGeolocationResult
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