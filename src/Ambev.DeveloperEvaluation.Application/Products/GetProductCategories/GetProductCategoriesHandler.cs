using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class GetProductCategoriesHandler : IRequestHandler<GetProductCategoriesCommand, List<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetProductCategoriesHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<List<string>> Handle(GetProductCategoriesCommand command, CancellationToken cancellationToken)
        => await _productRepository.GetProductCategoriesAsync(cancellationToken);
}
