namespace Store.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required string AccountId { get; init; }

        public required IReadOnlyList<OrderLine> Lines { get; init; } = [];

        public int TotalProducts => Lines.Sum(line => line.Quantity);

        public Price TotalPrice => Lines.Select(line => line.TotalPrice).Sum();

        public bool IsNotEmpty() => Lines.IsNotEmpty();
    }
}
