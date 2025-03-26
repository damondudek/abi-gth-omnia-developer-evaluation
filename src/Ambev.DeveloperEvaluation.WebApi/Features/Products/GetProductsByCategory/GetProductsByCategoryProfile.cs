using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

/// <summary>
/// Profile for mapping GetProductsByCategory feature requests to commands
/// </summary>
public class GetProductsByCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProductsByCategory feature
    /// </summary>
    public GetProductsByCategoryProfile()
    {
        //CreateMap<string, GetProductsByCategoryCommand>()
        //    .ConstructUsing(category => new GetProductsByCategoryCommand(category));
    }
}