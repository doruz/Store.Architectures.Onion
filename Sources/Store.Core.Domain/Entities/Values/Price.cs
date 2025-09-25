using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record Price
{
    public decimal Value { get; init; }
    public string Currency => "\u20ac"; // EURO symbol

    public Price(decimal value)
    {
        Value = EnsureArg.IsGte(value, 0, nameof(value));
    }

    public override string ToString() => $"{Value:F2} {Currency}";

    public static implicit operator Price(decimal priceValue) => new Price(priceValue);

    public static Price operator +(Price price1, Price price2) => new(price1.Value + price2.Value);
    public static Price operator *(Price price, int quantity) => new(price.Value * quantity);
}