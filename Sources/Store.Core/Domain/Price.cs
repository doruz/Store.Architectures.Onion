using EnsureThat;

namespace Store.Core.Domain
{
    public record Price
    {
        public double Value { get; init; }

        public string Currency { get; init; }

        public Price(double value, string currency)
        {
            Value = EnsureArg.IsGte(value, 0, nameof(value));
            Currency = EnsureArg.IsNotNullOrEmpty(currency, nameof(currency));
        }

        public override string ToString() => $"{Value:F2} {Currency}";

        public static Price Euro(double value) => new(value, "\u20ac");

        public static Price operator +(Price price1, Price price2)
        {
            EnsureArg.IsTrue(price1.Currency == price2.Currency);

            return new Price(price1.Value + price2.Value, price1.Currency);
        }

        public static Price operator *(Price price, int quantity)
            => price with { Value = price.Value * quantity };
    }

    public static class PriceExtensions
    {
        public static Price Sum<T>(this IEnumerable<T> values, Func<T, Price> priceSelector)
        {
            return values.Any() 
                ? values.Select(priceSelector).Aggregate((price1, price2) => price1 + price2) 
                : Price.Euro(0);
        }
    }
}