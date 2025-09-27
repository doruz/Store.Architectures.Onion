namespace Store.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required string AccountId { get; init; }

        public required IReadOnlyList<OrderProductDetails> Products { get; init; } = [];

        public Price TotalPrice => Products.Select(item => item.TotalPrice).Sum();

        public bool IsNotEmpty() => Products.IsNotEmpty();
    }

    public record OrderProductDetails
    {
        public required string Id { get; init; }

        public required string Name { get; init; }

        public required Price Price { get; init; }

        public required int Quantity { get; init; }

        public Price TotalPrice => Price * Quantity;
    }
}
