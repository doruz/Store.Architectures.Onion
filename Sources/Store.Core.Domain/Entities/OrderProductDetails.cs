namespace Store.Core.Domain.Entities;

public record OrderProductDetails
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required Price Price { get; init; }

    public required int Quantity { get; init; }

    public Price TotalPrice => Price * Quantity;
}