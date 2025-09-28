namespace Store.Core.Shared;

public static class ObjectExtensions
{
    public static TResult Map<T, TResult>(this T value, Func<T, TResult> mapper) => 
        value is null
            ? default
            : mapper(value);

    public static async Task<TResult> MapAsync<T, TResult>(this Task<T> value, Func<T, TResult> mapper)
        => (await value).Map(mapper);
}