using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating a cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a cart,
/// including the cart's ID, user ID, date, and products. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="UpdateCartResult"/>.
/// 
/// The data provided in this command is validated using the
/// <see cref="UpdateCartCommandValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class UpdateCartCommand : IRequest<UpdateCartResult>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CreateCartProductCommand> Products { get; set; } = new List<CreateCartProductCommand>();
}