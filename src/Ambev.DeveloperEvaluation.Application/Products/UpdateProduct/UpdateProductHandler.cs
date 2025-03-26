using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IProductRules _productRules;

    /// <summary>
    /// Initializes a new instance of UpdateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="productRules">The rules for product updates</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IProductRules productRules)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productRules = productRules;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await _productRules.CheckRulesToUpdate(command, cancellationToken);

        var product = _mapper.Map<Product>(command);
        var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);
        var result = _mapper.Map<UpdateProductResult>(updatedProduct);

        return result;
    }
}