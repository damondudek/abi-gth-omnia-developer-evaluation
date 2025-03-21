using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateUserResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateUserCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateUserCommand : IRequest<CreateUserResult>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public CreateUserName Name { get; set; }
    public CreateUserAddress Address { get; set; }
    public string Phone { get; set; }
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }

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

public class CreateUserName
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}

public class CreateUserAddress
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Zipcode { get; set; }
    public CreateUserAddressGeolocation? Geolocation { get; set; }
}

public class CreateUserAddressGeolocation
{
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
}