using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Command for retrieving carts
/// </summary>
public class GetCartsCommand : PaginatedCommand, IRequest<PaginatedResponse<GetCartResult>>
{
}