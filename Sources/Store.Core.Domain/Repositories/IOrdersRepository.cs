using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId);

    Task<Order?> FindOrderAsync(string accountId, string id);
}