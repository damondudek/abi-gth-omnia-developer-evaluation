using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Profile for mapping between User entity and UpdateUserResponse
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser operation
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, User>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.Name.LastName))
            .ForMember(d => d.AddressCity, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(d => d.AddressStreet, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(d => d.AddressNumber, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(d => d.AddressZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(d => d.AddressLat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(d => d.AddressLong, opt => opt.MapFrom(src => src.Address.Geolocation.Long));

        CreateMap<User, UpdateUserResult>()
            .IncludeBase<User, GetUserResult>();
    }
}
