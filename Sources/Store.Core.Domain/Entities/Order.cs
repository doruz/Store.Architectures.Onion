namespace Store.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required string AccountId { get; init; }

        public List<OrderProductDetails> Products { get; init; } = [];

        public Price TotalPrice => Products.Select(item => item.Price).Sum();
    }

    public record OrderProductDetails
    {
        public required string Id { get; init; }

        public required string Name { get; init; }

        public required Price Price { get; init; }

        public required int Quantity { get; init; }
    }
}
