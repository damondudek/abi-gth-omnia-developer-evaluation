using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Profile for mapping between Cart entity and CreateCartResponse
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart operation
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>();
        CreateMap<CreateCartProductCommand, CartProduct>();
        CreateMap<Cart, CreateCartResult>()
            .ForMember(d => d.Date, opt => opt.MapFrom(src => src.CreatedAt));
        CreateMap<CartProduct, CreateCartProductResult>();
    }
}