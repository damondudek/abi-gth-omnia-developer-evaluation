using Ambev.DeveloperEvaluation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class QueryableExtensions
{
    private static readonly char _coring = '*';

    public static async Task<PaginatedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, string orderBy, IDictionary<string, string> filters, CancellationToken cancellationToken)
    {
        query = query.ApplyOrderBy(orderBy);
        query = query.ApplyFilters(filters);
        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        var paginatedList = new PaginatedList<T>(items, count, pageNumber, pageSize);

        return paginatedList;
    }

    public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query, string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        var orderParams = orderBy.Split(',');
        var entityType = typeof(T);
        var properties = entityType.GetProperties();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyName = string.Empty;
            var orderDescending = false;
            if (param.EndsWith(" desc", StringComparison.OrdinalIgnoreCase))
            {
                propertyName = param[..^5];
                orderDescending = true;
            }

            if (param.EndsWith(" asc", StringComparison.OrdinalIgnoreCase))
                propertyName = param[..^4];

            propertyName = propertyName.Trim();
            if (string.IsNullOrEmpty(propertyName))
                continue;

            var property = properties.FirstOrDefault(p =>
                p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (property == null) continue;

            query = orderDescending
                ? query.OrderByDescending(property.Name)
                : query.OrderBy(property.Name);
        }

        return query;
    }

    private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDescending(ToLambda<T>(propertyName));
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IDictionary<string, string> filters)
    {
        var entityType = typeof(T);
        var properties = entityType.GetProperties();

        foreach (var filter in filters)
        {
            var value = filter.Value;
            var property = properties.FirstOrDefault(p => p.Name.Equals(filter.Key, StringComparison.OrdinalIgnoreCase));
            if (property == null)
                continue;

            if (value.EndsWith(_coring) || value.StartsWith(_coring))
            {
                value = value.Trim(_coring);
                query = query.Where(ContainsPredicate<T>(property.Name, value));
            }
            else if (property.PropertyType == typeof(string))
                query = query.Where(EqualsPredicate<T>(property.Name, value));
            else if (property.PropertyType.IsEnum)
            {
                var enumValue = Enum.Parse(property.PropertyType, value, true);
                query = query.Where(EqualsPredicate<T>(property.Name, enumValue));

            }
            else if (property.PropertyType == typeof(bool))
            {
                if (bool.TryParse(value, out var boolValue))
                    query = query.Where(EqualsPredicate<T>(property.Name, boolValue));
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                if (DateTime.TryParse(value, out var dateTimeValue))
                    query = query.Where(EqualsPredicate<T>(property.Name, dateTimeValue));
            }

            var isMin = filter.Key.StartsWith("_min");
            var isMax = filter.Key.StartsWith("_max");

            if (!isMin && !isMax) continue;

            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                if (DateTime.TryParse(value, out var dateTimeValue))
                {
                    if (isMin)
                        query = query.Where(GreaterThanOrEqualPredicate<T>(property.Name, dateTimeValue));
                    else
                        query = query.Where(LessThanOrEqualPredicate<T>(property.Name, dateTimeValue));
                }
            else if (decimal.TryParse(value, out var decimalValue))
                if (isMin)
                    query = query.Where(GreaterThanOrEqualPredicate<T>(property.Name, decimalValue));
                else
                    query = query.Where(LessThanOrEqualPredicate<T>(property.Name, decimalValue));
        }

        return query;
    }

    private static Expression<Func<T, bool>> EqualsPredicate<T>(string propertyName, object value)
    {
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var equal = Expression.Equal(property, constant);

        return Expression.Lambda<Func<T, bool>>(equal, parameter);
    }

    private static Expression<Func<T, bool>> ContainsPredicate<T>(string propertyName, string value)
    {
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
        var contains = Expression.Call(property, containsMethod, constant);
        
        return Expression.Lambda<Func<T, bool>>(contains, parameter);
    }

    private static Expression<Func<T, bool>> GreaterThanOrEqualPredicate<T>(string propertyName, object value)
    {
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, constant);

        return Expression.Lambda<Func<T, bool>>(greaterThanOrEqual, parameter);
    }

    private static Expression<Func<T, bool>> LessThanOrEqualPredicate<T>(string propertyName, object value)
    {
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var lessThanOrEqual = Expression.LessThanOrEqual(property, constant);

        return Expression.Lambda<Func<T, bool>>(lessThanOrEqual, parameter);
    }

    private static (ParameterExpression, MemberExpression, ConstantExpression) GetExpressions<T>(string propertyName, object value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value, property.Type);

        return (parameter, property, constant);
    }
}
