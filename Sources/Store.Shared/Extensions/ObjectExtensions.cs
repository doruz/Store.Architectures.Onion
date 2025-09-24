namespace Store.Shared.Extensions;

public static class ObjectExtensions
{
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult> mapper) => 
        value is null
            ? default
            : mapper(value);

    public static async Task<TResult?> MapAsync<T, TResult>(this Task<T?> value, Func<T, TResult> mapper)
        => (await value).Map(mapper);
}

public static class EnumerableExtensions
{
    public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> values, Func<T, TResult> mapper) 
        => (await values).Select(mapper);
}