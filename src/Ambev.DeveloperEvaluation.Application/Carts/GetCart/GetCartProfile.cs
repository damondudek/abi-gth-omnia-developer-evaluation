using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Profile for mapping between Cart entity and GetCartResponse
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart operation
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResult>()
            .ForMember(d => d.Date, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<PaginatedList<Cart>, PaginatedResponse<GetCartResult>>()
            .ForMember(d => d.Data, opt => opt.MapFrom(src => src.ToList()))
            .ForMember(d => d.TotalItems, opt => opt.MapFrom(src => src.TotalCount));
        
        CreateMap<CartProduct, GetCartProductResult>();
    }
}