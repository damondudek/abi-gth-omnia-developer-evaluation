using Ambev.DeveloperEvaluation.Domain.Models;

public static class IQueryableExtensions
{
    public static Task<PaginatedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
        => PaginatedList<T>.CreateAsync(source, pageNumber, pageSize, cancellationToken);

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int size)
        => source.Skip((page - 1) * size).Take(size);
}
