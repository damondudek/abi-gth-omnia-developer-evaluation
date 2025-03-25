namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserAddressGeolocationRequest
{
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
}