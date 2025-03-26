using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Profile for mapping between Product entity and GetProductResponse
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
            .ForPath(d => d.Rating.Rate, opt => opt.MapFrom(src => src.AverageRating))
            .ForPath(d => d.Rating.Count, opt => opt.MapFrom(src => src.RatingCount));

        CreateMap<PaginatedList<Product>, PaginatedResponse<GetProductResult>>()
            .ForMember(d => d.Data, opt => opt.MapFrom(src => src.ToList()))
            .ForMember(d => d.TotalItems, opt => opt.MapFrom(src => src.TotalCount));
    }
}