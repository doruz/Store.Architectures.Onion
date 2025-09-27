using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosOrdersRepository(CosmosDatabaseContainers containers) : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId)
    {
        EnsureArg.IsNotNullOrEmpty(accountId, nameof(accountId));

        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = accountId.ToPartitionKey()
        };

        var orders = containers.Orders
            .GetItemLinqQueryable<Order>(true, requestOptions: requestOptions)
            .AsEnumerable();

        return Task.FromResult(orders);
    }

    public Task<Order?> FindOrderAsync(string accountId, string id) 
        => containers.Orders.FindAsync<Order>(id, accountId.ToPartitionKey());

    public async Task SaveOrderAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        await containers.Orders.UpsertItemAsync(order);
    }
}