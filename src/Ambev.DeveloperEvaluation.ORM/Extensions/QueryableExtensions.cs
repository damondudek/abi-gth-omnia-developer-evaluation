using Ambev.DeveloperEvaluation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class QueryableExtensions
{
    private static readonly char _likeOp = '*';
    private static readonly string _minOp = "_min";
    private static readonly string _maxOp = "_max";
    private static readonly string _descOrder = " desc";
    private static readonly string _ascOrder = " asc";

    public static async Task<PaginatedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, string orderBy, IDictionary<string, string> filters, CancellationToken cancellationToken)
    {
        query = query.ApplyFilters(filters);
        var count = await query.CountAsync(cancellationToken);
        
        query = query.ApplyOrderBy(orderBy);
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
            if (param.EndsWith(_descOrder, StringComparison.OrdinalIgnoreCase))
            {
                propertyName = param[..^5];
                orderDescending = true;
            } else if (param.EndsWith(_ascOrder, StringComparison.OrdinalIgnoreCase))
                propertyName = param[..^4];

            propertyName = propertyName.Trim();
            if (string.IsNullOrEmpty(propertyName))
                continue;

            var property = properties.FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            if (property == null) continue;

            query = orderDescending
                ? query.OrderByDescending(property.Name)
                : query.OrderBy(property.Name);
        }

        return query;
    }
        
    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IDictionary<string, string> filters)
    {
        var entityType = typeof(T);
        var properties = entityType.GetProperties();

        foreach (var filter in filters)
        {
            var value = filter.Value.Trim();
            var key = filter.Key.Trim();
            var isRangeFilter = false;
            var isMinRangeFilter = false;

            if (key.StartsWith(_minOp) || filter.Key.StartsWith(_maxOp))
            {
                isRangeFilter = true;
                isMinRangeFilter = key.StartsWith(_minOp);
                key = key.Substring(_minOp.Length);
            }

            var property = properties.FirstOrDefault(p => p.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (property == null)
                continue;

            if (isRangeFilter)
                query = ApplyRangeFilter(query, value, property, isMinRangeFilter);
            else
                query = ApplyFilter(query, value, property);
        }

        return query;
    }

    private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string value, PropertyInfo property)
    {
        if (value.StartsWith(_likeOp) || value.EndsWith(_likeOp))
        {
            if (value.StartsWith(_likeOp) && value.EndsWith(_likeOp))
                query = query.Where(ContainsPredicate<T>(property.Name, value));
            else if (value.StartsWith(_likeOp))
                query = query.Where(StartsWithPredicate<T>(property.Name, value));
            else if (value.EndsWith(_likeOp))
                query = query.Where(EndsWithPredicate<T>(property.Name, value));
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

        return query;
    }

    private static IQueryable<T> ApplyRangeFilter<T>(IQueryable<T> query, string value, PropertyInfo property, bool isMin)
    {
        if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
        {
            if (DateTime.TryParse(value, out var dateTimeValue))
            {
                if (isMin)
                    query = query.Where(GreaterThanOrEqualPredicate<T>(property.Name, dateTimeValue));
                else
                    query = query.Where(LessThanOrEqualPredicate<T>(property.Name, dateTimeValue));
            }
        }
        else if (property.PropertyType == typeof(decimal) ||
                property.PropertyType == typeof(decimal?) ||
                property.PropertyType == typeof(int) ||
                property.PropertyType == typeof(int?) ||
                property.PropertyType == typeof(double) ||
                property.PropertyType == typeof(double?) ||
                property.PropertyType == typeof(float) ||
                property.PropertyType == typeof(float?))
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                if (isMin)
                    query = query.Where(GreaterThanOrEqualPredicate<T>(property.Name, decimalValue));
                else
                    query = query.Where(LessThanOrEqualPredicate<T>(property.Name, decimalValue));
            }
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
        value = value.Trim(_likeOp);
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var method = typeof(string).GetMethod("Contains", [typeof(string)]);
        var expression = Expression.Call(property, method, constant);
        
        return Expression.Lambda<Func<T, bool>>(expression, parameter);
    }

    private static Expression<Func<T, bool>> StartsWithPredicate<T>(string propertyName, string value)
    {
        value = value.Trim(_likeOp);
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var method = typeof(string).GetMethod("StartsWith", [typeof(string)]);
        var expression = Expression.Call(property, method, constant);

        return Expression.Lambda<Func<T, bool>>(expression, parameter);
    }

    private static Expression<Func<T, bool>> EndsWithPredicate<T>(string propertyName, string value)
    {
        value = value.Trim(_likeOp);
        var (parameter, property, constant) = GetExpressions<T>(propertyName, value);
        var method = typeof(string).GetMethod("EndsWith", [typeof(string)]);
        var expression = Expression.Call(property, method, constant);

        return Expression.Lambda<Func<T, bool>>(expression, parameter);
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
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value, property.Type);

        return (parameter, property, constant);
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        => query.OrderBy(ToLambda<T>(propertyName));

    private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        => query.OrderByDescending(ToLambda<T>(propertyName));
}
