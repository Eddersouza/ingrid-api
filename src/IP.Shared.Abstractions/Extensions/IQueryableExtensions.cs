namespace IP.Shared.Abstractions.Extensions;

public enum IQueryableDirection { ASC, DESC }

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> queryable,
        Dictionary<string, Expression<Func<T, object>>> sortDictionary,
        string[]? sortItems = null,
        string[]? defaultSort = null)
    {
        if ((sortItems is null || sortItems.Length == 0) && 
            (defaultSort is null || defaultSort.Length == 0)) return queryable;

        if ((sortItems is null || sortItems.Length == 0) &&
            (defaultSort is not null && defaultSort.Length > 0))
            sortItems = defaultSort;

        foreach (string sortItem in sortItems!)
            queryable = queryable.CreateOrderBy(sortDictionary, sortItem);

        return queryable;
    }
    private static IQueryable<T> CreateOrderBy<T>(
       this IQueryable<T> queryable,
       Dictionary<string, Expression<Func<T, object>>> sortDictionary,
       string sortItem)
    {
        string[] sortItemSplit = sortItem.Split(':');        

        IQueryableDirection direction = sortItemSplit.Length > 1 ?
            sortItemSplit[1].ToUpper().ToEnumSafe(IQueryableDirection.ASC) :
            IQueryableDirection.ASC;
        
            queryable = direction == IQueryableDirection.ASC
                ? queryable.SmartOrderBy(sortDictionary[sortItemSplit[0]])
                : queryable.SmartOrderByDescending(sortDictionary[sortItemSplit[0]]);

        return queryable;
    }

    private static bool IsOrdered<T>(this IQueryable<T> queryable)
    {
        return queryable == null ?
            throw new ArgumentNullException(nameof(queryable)) :
            queryable.Expression.Type == typeof(IOrderedQueryable<T>);
    }

    public static IQueryable<T> SmartOrderBy<T, TKey>(
        this IQueryable<T> queryable,
        Expression<Func<T, TKey>> keySelector)
    {
        return queryable.IsOrdered()
        ? ((IOrderedQueryable<T>)queryable).ThenBy(keySelector)
        : queryable.OrderBy(keySelector);
    }

    public static IQueryable<T> SmartOrderByDescending<T, TKey>(
        this IQueryable<T> queryable,
        Expression<Func<T, TKey>> keySelector)
    {
        return queryable.IsOrdered()
        ? ((IOrderedQueryable<T>)queryable).ThenByDescending(keySelector)
        : queryable.OrderByDescending(keySelector);
    }

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate) =>
            condition ? queryable.Where(predicate) : queryable;

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}