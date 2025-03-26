using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler for processing UpdateCartCommand requests
/// </summary>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateCartHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateCartCommand request
    /// </summary>
    /// <param name="command">The UpdateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        // Uncomment and implement validation logic if necessary
        // var validator = new UpdateCartCommandValidator();
        // var validationResult = await validator.ValidateAsync(command, cancellationToken);

        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        var cart = _mapper.Map<Cart>(command);
        var updatedCart = await _cartRepository.UpdateAsync(cart, cancellationToken);
        var result = _mapper.Map<UpdateCartResult>(updatedCart);

        return result;
    }
}