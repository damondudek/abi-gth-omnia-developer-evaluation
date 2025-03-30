using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.Name.LastName))
            .ForMember(d => d.AddressCity, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(d => d.AddressStreet, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(d => d.AddressNumber, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(d => d.AddressZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(d => d.AddressLat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(d => d.AddressLong, opt => opt.MapFrom(src => src.Address.Geolocation.Long));

        CreateMap<User, CreateUserResult>()
            .IncludeBase<User, GetUserResult>();

    }
}
