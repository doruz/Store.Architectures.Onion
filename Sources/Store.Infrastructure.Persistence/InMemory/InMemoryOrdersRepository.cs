using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryOrdersRepository(InMemoryDatabase database) : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId)
    {
        EnsureArg.IsNotNullOrEmpty(accountId, nameof(accountId));

        var accountsOrders = database.Orders
            .Where(order => order.AccountId.IsEqualTo(accountId));

        return Task.FromResult(accountsOrders);
    }

    public Task<Order?> FindOrderAsync(string accountId, string id)
    {
        EnsureArg.IsNotNullOrEmpty(accountId, nameof(accountId));
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        var accountOrder = database.Orders
            .FirstOrDefault(order => order.AccountId.IsEqualTo(accountId) && order.Id.IsEqualTo(id));

        return Task.FromResult(accountOrder);
    }

    public Task SaveOrderAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        database.Orders.Add(order);

        return Task.CompletedTask;
    }
}