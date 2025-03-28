using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.WebApi.Features.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Filters;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">The product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiDataResponse<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var requestCommand = _mapper.Map<CreateProductCommand>(request);
        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);

        var response = _mapper.Map<CreateProductResponse>(responseCommand);
        var apiResponse = new ApiDataResponse<CreateProductResponse>(response, ProductsMessage.ProductCreatedSuccess);

        return Created(string.Empty, apiResponse);
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="request">The product update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiDataResponse<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var requestCommand = _mapper.Map<UpdateProductCommand>(request);
        requestCommand.Id = id;

        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);

        var response = _mapper.Map<UpdateProductResponse>(responseCommand);
        var apiResponse = new ApiDataResponse<UpdateProductResponse>(response, ProductsMessage.ProductUpdatedSuccess);

        return Ok(apiResponse);
    }

    /// <summary>
    /// Retrieves products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    [FilterQuery]
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromQuery] QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var requestCommand = _mapper.Map<GetProductsCommand>(queryParameters);
        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);
        var response = _mapper.Map<PaginatedResponse<GetProductResponse>>(responseCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The procuts details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiDataResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var requestCommand = _mapper.Map<GetProductCommand>(request.Id);
            var responseCommand = await _mediator.Send(requestCommand, cancellationToken);
            var response = _mapper.Map<GetProductResponse>(responseCommand);
            var apiResponse = new ApiDataResponse<GetProductResponse>(response, ProductsMessage.ProductRetrievedSuccess);

            return Ok(apiResponse);

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ProductsMessage.ProductNotFound, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);
        var apiResponse = new ApiResponse(ProductsMessage.ProductDeletedSuccess);

        return Ok(apiResponse);
    }

    /// <summary>
    /// Retrieves product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product categories</returns>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductCategories(CancellationToken cancellationToken)
    {
        var requestCommand = new GetProductCategoriesCommand();
        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);

        return Ok(responseCommand);
    }

    /// <summary>
    /// Retrieves products by category
    /// </summary>
    /// <param name="category">The unique identifier of the category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Products from category</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductsByCategory([FromRoute] string category, [FromQuery] QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        //var validator = new GetProductsByCategoryRequestValidator();
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //if (!validationResult.IsValid)
        //    return BadRequest(validationResult.Errors);

        var requestCommand = _mapper.Map<GetProductsByCategoryCommand>(queryParameters);
        requestCommand.Category = category;

        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);
        var response = _mapper.Map<PaginatedResponse<GetProductResponse>>(responseCommand);

        return Ok(response);
    }
}
