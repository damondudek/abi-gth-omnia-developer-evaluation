using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

/// <summary>
/// Handler for processing GetProductCommand requests
/// </summary>
public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryCommand, PaginatedResponse<GetProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetProductByCategoryHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetProductsByCategoryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductByCategoryCommand request
    /// </summary>
    /// <param name="request">The GetProductByCategory command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    public async Task<PaginatedResponse<GetProductResult>> Handle(GetProductsByCategoryCommand request, CancellationToken cancellationToken)
    {
        //var validator = new GetProductsByCategoryValidator();
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //if (!validationResult.IsValid)
        //    throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.GetPaginatedByCategoryAsync(request.Category, request.PageNumber, request.PageSize, cancellationToken);

        return _mapper.Map<PaginatedResponse<GetProductResult>>(products);
    }
}