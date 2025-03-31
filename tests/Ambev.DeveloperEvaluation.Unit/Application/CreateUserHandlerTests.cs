using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateUserHandler"/> class.
/// </summary>
public class CreateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRules _userRules;
    private readonly CreateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _userRules = Substitute.For<IUserRules>();
        _handler = new CreateUserHandler(_userRepository, _mapper, _passwordHasher, _userRules);
    }

    /// <summary>
    /// Tests that a valid user creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid user data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Username = command.Username,
            Password = command.Password,
            FirstName = command.Name.FirstName,
            LastName = command.Name.LastName,
            AddressCity = command.Address.City,
            AddressStreet = command.Address.Street,
            AddressNumber = command.Address.Number,
            AddressZipCode = command.Address.ZipCode,
            AddressLat = command.Address.Geolocation.Lat,
            AddressLong = command.Address.Geolocation.Long,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };
        var result = new CreateUserResult
        {
            Id = user.Id
        };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<CreateUserResult>(user).Returns(result);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // Act
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        createUserResult.Should().NotBeNull();
        createUserResult.Id.Should().Be(user.Id);
        await _userRepository.Received(1).CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid user creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid user data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateUserCommand(); // Missing required properties

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the password is hashed before saving the user.
    /// </summary>
    [Fact(DisplayName = "Given user creation request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();

        var originalPassword = command.Password;
        const string hashedPassword = "h@shedPassw0rd";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Username = command.Username,
            Password = originalPassword,
            FirstName = command.Name.FirstName,
            LastName = command.Name.LastName,
            AddressCity = command.Address.City,
            AddressStreet = command.Address.Street,
            AddressNumber = command.Address.Number,
            AddressZipCode = command.Address.ZipCode,
            AddressLat = command.Address.Geolocation.Lat,
            AddressLong = command.Address.Geolocation.Long,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.HashPassword(originalPassword).Returns(hashedPassword);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _passwordHasher.Received(1).HashPassword(originalPassword);
        await _userRepository.Received(1).CreateAsync(
            Arg.Is<User>(u => u.Password == hashedPassword),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Username = command.Username,
            Password = command.Password,
            FirstName = command.Name.FirstName,
            LastName = command.Name.LastName,
            AddressCity = command.Address.City,
            AddressStreet = command.Address.Street,
            AddressNumber = command.Address.Number,
            AddressZipCode = command.Address.ZipCode,
            AddressLat = command.Address.Geolocation.Lat,
            AddressLong = command.Address.Geolocation.Long,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<User>(Arg.Is<CreateUserCommand>(c =>
            c.Email == command.Email &&
            c.Username == command.Username &&
            c.Password == command.Password &&
            c.Name == command.Name &&
            c.Address == command.Address &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}