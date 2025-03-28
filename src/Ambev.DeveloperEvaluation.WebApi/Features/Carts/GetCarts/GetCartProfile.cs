using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Domain.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// Profile for mapping GetCarts feature requests to commands
/// </summary>
public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCarts feature
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<QueryParameters, GetCartsCommand>();
    }
}