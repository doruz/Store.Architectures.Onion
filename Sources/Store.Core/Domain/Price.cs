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

        public static Price operator *(Price price, int quantity)
            => price with { Value = price.Value * quantity };
    }
}