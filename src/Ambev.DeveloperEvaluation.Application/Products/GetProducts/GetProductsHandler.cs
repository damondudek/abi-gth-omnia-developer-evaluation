using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Handler for processing GetProductsCommand requests
/// </summary>
public class GetProductsHandler : IRequestHandler<GetProductsCommand, PaginatedResponse<GetProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetProductsHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductsCommand request
    /// </summary>
    /// <param name="request">The GetProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details</returns>
    public async Task<PaginatedResponse<GetProductResult>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        //var validator = new GetProductsValidator();
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //if (!validationResult.IsValid)
        //    throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, request.OrderBy, request.Filters, cancellationToken);

        return _mapper.Map<PaginatedResponse<GetProductResult>>(products);
    }
}