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
        CreateMap<User, GetUserResult>();
        CreateMap<PaginatedList<User>, PaginatedResponse<GetUserResult>>()
            .ForMember(d => d.Data, opt => opt.MapFrom(src => src.ToList()))
            .ForMember(d => d.TotalItems, opt => opt.MapFrom(src => src.TotalCount));
    }
}
