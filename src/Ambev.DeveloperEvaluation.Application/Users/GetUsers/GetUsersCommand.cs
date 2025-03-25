using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;

/// <summary>
/// Command for retrieving users
/// </summary>
public class GetUsersCommand : PaginatedCommand, IRequest<PaginatedResponse<GetUserResult>>
{
}
