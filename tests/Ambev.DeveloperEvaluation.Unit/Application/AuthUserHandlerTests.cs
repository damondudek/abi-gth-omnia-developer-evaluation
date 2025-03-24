using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

/// <summary>
/// Contains unit tests for the <see cref="AuthenticateUserHandler"/> class.
/// </summary>
public class AuthenticateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AuthenticateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserHandlerTests"/> class.
    /// Sets up test dependencies and mock objects.
    /// </summary>
    public AuthenticateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
    }

    /// <summary>
    /// Tests that valid credentials return a valid authentication result.
    /// </summary>
    [Fact(DisplayName = "Given valid credentials When authenticating Then returns valid token")]
    public async Task Handle_ValidCredentials_ReturnsValidToken()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            Username = "validUser",
            Password = "validPassword"
        };

        var user = new User
        {
            Username = command.Username,
            Password = "hashedPassword",
            Status = UserStatus.Active
        };

        const string generatedToken = "validToken";

        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _jwtTokenGenerator.GenerateToken(user).Returns(generatedToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(generatedToken);

        await _userRepository.Received(1).GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(command.Password, user.Password);
        _jwtTokenGenerator.Received(1).GenerateToken(user);
    }

    /// <summary>
    /// Tests that invalid credentials throw an UnauthorizedAccessException.
    /// </summary>
    [Fact(DisplayName = "Given invalid credentials When authenticating Then throws unauthorized exception")]
    public async Task Handle_InvalidCredentials_ThrowsUnauthorizedException()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            Username = "invalidUser",
            Password = "invalidPassword"
        };

        var user = new User
        {
            Username = command.Username,
            Password = "hashedPassword",
            Status = UserStatus.Active
        };

        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(false);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid credentials. Please try again with different credentials");

        await _userRepository.Received(1).GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(command.Password, user.Password);
        _jwtTokenGenerator.DidNotReceive().GenerateToken(Arg.Any<User>());
    }

    /// <summary>
    /// Tests that an inactive user throws an UnauthorizedAccessException.
    /// </summary>
    [Fact(DisplayName = "Given inactive user When authenticating Then throws unauthorized exception")]
    public async Task Handle_InactiveUser_ThrowsUnauthorizedException()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            Username = "inactiveUser",
            Password = "validPassword"
        };

        var user = new User
        {
            Username = command.Username,
            Password = "hashedPassword",
            Status = UserStatus.Inactive // User is inactive
        };

        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("User is not active");

        await _userRepository.Received(1).GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(command.Password, user.Password);
        _jwtTokenGenerator.DidNotReceive().GenerateToken(Arg.Any<User>());
    }

    /// <summary>
    /// Tests that a non-existent user throws an UnauthorizedAccessException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user When authenticating Then throws unauthorized exception")]
    public async Task Handle_UserNotFound_ThrowsUnauthorizedException()
    {
        // Arrange
        var command = new AuthenticateUserCommand
        {
            Username = "nonExistentUser",
            Password = "password"
        };

        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid credentials. Please try again with different credentials");

        await _userRepository.Received(1).GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>());
        _passwordHasher.DidNotReceive().VerifyPassword(Arg.Any<string>(), Arg.Any<string>());
        _jwtTokenGenerator.DidNotReceive().GenerateToken(Arg.Any<User>());
    }
}
