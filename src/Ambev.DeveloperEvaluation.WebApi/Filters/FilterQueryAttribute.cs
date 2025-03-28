using Ambev.DeveloperEvaluation.Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;

namespace Ambev.DeveloperEvaluation.WebApi.Filters;

public class FilterQueryAttribute : ActionFilterAttribute
{
    private static readonly HashSet<string> _reservedParameters = ["_order", "_page", "_size"];
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var queryString = context.HttpContext.Request.QueryString.ToString().Trim();

        if (string.IsNullOrEmpty(queryString))
        {
            base.OnActionExecuting(context);
            return;
        }

        var filters = ParseQueryFilters(queryString);

        if (context.ActionArguments.TryGetValue(nameof(QueryParameters), out object? value) &&
            value is QueryParameters queryParameters)
            queryParameters.Filters = filters;

        base.OnActionExecuting(context);
    }

    private static Dictionary<string, string> ParseQueryFilters(string queryString)
    {
        var queryDictionary = HttpUtility.ParseQueryString(queryString);
        var queryParameters = new Dictionary<string, string>();

        foreach (var key in queryDictionary.AllKeys.Where(key => key != null))
        {
            if (!_reservedParameters.Contains(key!) && !string.IsNullOrWhiteSpace(queryDictionary[key]))
                queryParameters[key!] = queryDictionary[key]!;
        }

        return queryParameters;
    }
}
