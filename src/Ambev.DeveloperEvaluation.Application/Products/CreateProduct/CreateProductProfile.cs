using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Product entity and CreateProductResult
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(d => d.RatingCount, opt => opt.MapFrom(src => src.Rating.Count))
            .ForMember(d => d.AverageRating, opt => opt.MapFrom(src => src.Rating.Rate));
        CreateMap<Product, CreateProductResult>()
            .ForPath(d => d.Rating.Rate, opt => opt.MapFrom(src => src.AverageRating))
            .ForPath(d => d.Rating.Count, opt => opt.MapFrom(src => src.RatingCount));
            
    }
}
