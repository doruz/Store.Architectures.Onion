namespace Store.Core.Domain.Entities;

public static class PriceExtensions
{
    public static Price Sum<T>(this IEnumerable<T> items, Func<T, Price> priceSelector)
        => items.Select(priceSelector).Sum();

    public static Price Sum(this IEnumerable<Price> prices)
    {
        return prices.Any()
            ? prices.Aggregate((price1, price2) => price1 + price2)
            : 0;
    }
}