using Ambev.DeveloperEvaluation.Domain.Const;
using Ambev.DeveloperEvaluation.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int GetCurrentUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult Ok<T>(T data, string message) =>
            base.Ok(new ApiDataResponse<T>(data, message));

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiDataResponse<T> { Data = data });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse(message, false));

    protected IActionResult NotFound(string error, string detail, string type = ErrorType.ResourceNotFound) =>
        base.NotFound(new
        {
            type,
            error,
            detail
        });

    protected IActionResult Unauthorized(string error, string detail, string type = ErrorType.AuthenticationError) =>
        base.NotFound(new
        {
            type,
            error,
            detail
        });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList, string message = "") =>
            Ok(new ApiPaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Message = message
            });
}
