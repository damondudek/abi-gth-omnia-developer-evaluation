using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping GetCart feature requests to commands
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart feature
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Guid, GetCartCommand>()
            .ConstructUsing(id => new GetCartCommand(id));
        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<GetCartProductResult, GetCartProductResponse>();
        CreateMap<PaginatedResponse<GetCartResult>, PaginatedResponse<GetCartResponse>>();
    }
}