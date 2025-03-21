using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;

/// <summary>
/// Command for retrieving users
/// </summary>
public record GetUsersCommand : IRequest<List<GetUserResult>>
{
}
