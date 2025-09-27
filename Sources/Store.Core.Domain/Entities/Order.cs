using EnsureThat;

namespace Store.Core.Domain.Entities;

public class Order : BaseEntity
{
    public required string AccountId { get; init; }

    public required IReadOnlyList<OrderLine> Lines { get; init; } = [];

    public int TotalProducts => Lines.Sum(line => line.Quantity);

    public Price TotalPrice => Lines.Select(line => line.TotalPrice).Sum();

    public bool IsNotEmpty() => Lines.IsNotEmpty();

    public static Order Create(string accountId, params IEnumerable<OrderLine> lines) => new()
    {
        AccountId = EnsureArg.IsNotNullOrEmpty(accountId, nameof(accountId)),
        Lines = Ensure.Enumerable.HasItems(lines).ToList()
    };
}