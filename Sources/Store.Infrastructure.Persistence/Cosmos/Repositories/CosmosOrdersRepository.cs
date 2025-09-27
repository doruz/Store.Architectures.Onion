using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosOrdersRepository : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> FindOrderAsync(string accountId, string id)
    {
        throw new NotImplementedException();
    }
}