namespace Store.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required string AccountId { get; init; }

        public required IReadOnlyList<OrderProductDetails> Products { get; init; } = [];

        public Price TotalPrice => Products.Select(item => item.TotalPrice).Sum();

        public bool IsNotEmpty() => Products.IsNotEmpty();
    }
}
