﻿using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
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
    /// Retrieves product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product categories</returns>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
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
        var request = new GetProductsByCategoryCommand
        { 
            Category = category,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
        };
        //var validator = new GetProductsByCategoryRequestValidator();
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //if (!validationResult.IsValid)
        //    return BadRequest(validationResult.Errors);

        //var requestCommand = _mapper.Map<GetProductsByCategoryCommand>(request);
        var responseCommand = await _mediator.Send(request, cancellationToken);
        var response = _mapper.Map<PaginatedResponse<GetProductResponse>>(responseCommand);

        return Ok(response);
    }
}
