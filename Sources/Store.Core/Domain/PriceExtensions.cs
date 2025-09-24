namespace Store.Core.Domain;

public static class PriceExtensions
{
    public static Price Sum<T>(this IEnumerable<T> values, Func<T, Price> priceSelector)
    {
        return values.Any() 
            ? values.Select(priceSelector).Aggregate((price1, price2) => price1 + price2) 
            : Price.Euro(0);
    }
}