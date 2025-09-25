namespace Store.Shared.Extensions;

public static class EnumerableExtensions
{
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