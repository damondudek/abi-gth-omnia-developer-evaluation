using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>()
           .ForPath(d => d.Name.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForPath(d => d.Name.LastName, opt => opt.MapFrom(src => src.LastName))
           .ForPath(d => d.Address.City, opt => opt.MapFrom(src => src.AddressCity))
           .ForPath(d => d.Address.Street, opt => opt.MapFrom(src => src.AddressStreet))
           .ForPath(d => d.Address.Number, opt => opt.MapFrom(src => src.AddressNumber))
           .ForPath(d => d.Address.ZipCode, opt => opt.MapFrom(src => src.AddressCity))
           .ForPath(d => d.Address.Geolocation.Lat, opt => opt.MapFrom(src => src.AddressLat))
           .ForPath(d => d.Address.Geolocation.Long, opt => opt.MapFrom(src => src.AddressLong));

        CreateMap<PaginatedList<User>, PaginatedResponse<GetUserResult>>()
            .ForMember(d => d.Data, opt => opt.MapFrom(src => src.ToList()))
            .ForMember(d => d.TotalItems, opt => opt.MapFrom(src => src.TotalCount));
    }
}
