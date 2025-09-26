namespace Store.Core.Shared;

public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T>? items)
        => items is null || items.Any() is false;

    public static bool IsNotEmpty<T>(this IEnumerable<T>? items)
        => !items.IsEmpty();

    public static List<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        var itemsList = items.ToList();

        itemsList.ForEach(action);

        return itemsList;
    }
}