using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(d => d.RatingCount, opt => opt.MapFrom(src => src.Rating.Count))
            .ForMember(d => d.AverageRating, opt => opt.MapFrom(src => src.Rating.Rate));
        CreateMap<Product, UpdateProductResult>()
            .ForPath(d => d.Rating.Rate, opt => opt.MapFrom(src => src.AverageRating))
            .ForPath(d => d.Rating.Count, opt => opt.MapFrom(src => src.RatingCount));
    }
}