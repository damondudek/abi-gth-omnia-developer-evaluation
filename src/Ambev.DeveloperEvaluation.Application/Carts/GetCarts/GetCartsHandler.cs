using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Handler for processing GetCartsCommand requests
/// </summary>
public class GetCartsHandler : IRequestHandler<GetCartsCommand, PaginatedResponse<GetCartResult>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartsHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetCartsHandler(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartsCommand request
    /// </summary>
    /// <param name="request">The GetCarts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details</returns>
    public async Task<PaginatedResponse<GetCartResult>> Handle(GetCartsCommand request, CancellationToken cancellationToken)
    {
        // Uncomment and implement validation logic if necessary
        // var validator = new GetCartsValidator();
        // var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        var carts = await _cartRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);

        return _mapper.Map<PaginatedResponse<GetCartResult>>(carts);
    }
}