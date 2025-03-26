using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.WebApi.Features.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing cart operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CartsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiDataResponse<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var requestCommand = _mapper.Map<CreateCartCommand>(request);
        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);

        var response = _mapper.Map<CreateCartResponse>(responseCommand);
        var apiResponse = new ApiDataResponse<CreateCartResponse>(response, CartsMessage.CartCreatedSuccess);

        return Created(string.Empty, apiResponse);
    }

    /// <summary>
    /// Updates a cart
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="request">The cart update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiDataResponse<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCart([FromRoute] Guid id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var requestCommand = _mapper.Map<UpdateCartCommand>(request);
        requestCommand.Id = id;

        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);

        var response = _mapper.Map<UpdateCartResponse>(responseCommand);
        var apiResponse = new ApiDataResponse<UpdateCartResponse>(response, CartsMessage.CartUpdatedSuccess);

        return Ok(apiResponse);
    }

    /// <summary>
    /// Retrieves carts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarts([FromQuery] QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var requestCommand = new GetCartsCommand()
        {
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
        };
        var responseCommand = await _mediator.Send(requestCommand, cancellationToken);
        var response = _mapper.Map<PaginatedResponse<GetCartResponse>>(responseCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiDataResponse<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var requestCommand = _mapper.Map<GetCartCommand>(request.Id);
            var responseCommand = await _mediator.Send(requestCommand, cancellationToken);
            var response = _mapper.Map<GetCartResponse>(responseCommand);
            var apiResponse = new ApiDataResponse<GetCartResponse>(response, CartsMessage.CartRetrievedSuccess);

            return Ok(apiResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(CartsMessage.CartNotFound, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the cart was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);
        var apiResponse = new ApiResponse(CartsMessage.CartDeletedSuccess);

        return Ok(apiResponse);
    }
}