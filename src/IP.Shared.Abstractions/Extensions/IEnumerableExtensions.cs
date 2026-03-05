namespace IP.Shared.Abstractions.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<TSource> FromHierarchy<TSource>(
        this TSource source,
        Func<TSource, TSource> nextItem,
        Func<TSource, bool> canContinue)
    {
        while (canContinue(source))
        {
            yield return source;
            source = nextItem(source);
        }
    }

    public static IEnumerable<TSource> FromHierarchy<TSource>(
        this TSource source,
        Func<TSource, TSource> nextItem)
        where TSource : class
    {
        return source.FromHierarchy(nextItem, s => s != null);
    }
}