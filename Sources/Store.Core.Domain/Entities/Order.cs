using EnsureThat;

namespace Store.Core.Domain.Entities;

    public class Order : BaseEntity
    {
        public required string CustomerId { get; init; }

        public required IReadOnlyList<OrderLine> Lines { get; init; } = [];

        public int TotalProducts => Lines.Sum(line => line.Quantity);

        public Price TotalPrice => Lines.Select(line => line.TotalPrice).Sum();

        public bool IsNotEmpty() => Lines.IsNotEmpty();

    public static Order Create(string customerId, params IEnumerable<OrderLine> lines) => new()
    {
        CustomerId = EnsureArg.IsNotNullOrEmpty(customerId),
        Lines = Ensure.Enumerable.HasItems(lines).ToList()
    };
}