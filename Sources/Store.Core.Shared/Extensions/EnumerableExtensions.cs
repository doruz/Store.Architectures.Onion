namespace Store.Core.Shared;

public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T>? items)
        => items is null || items.Any() is false;

    public static List<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        var itemsList = items.ToList();

        itemsList.ForEach(action);

        return itemsList;
    }

    public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> values, Func<T, TResult> mapper) 
        => (await values).Select(mapper);

    public static async Task<List<T>> ToListAsync<T>(this IEnumerable<Task<T>> valuesTasks)
    {
        var result = new List<T>();

        foreach (var valueTask in valuesTasks)
        {
            result.Add(await valueTask);
        }

        return result;
    }
}