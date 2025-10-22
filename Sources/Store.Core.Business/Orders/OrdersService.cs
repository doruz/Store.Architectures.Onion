using Store.Core.Business.Errors;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

public sealed class OrdersService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task<IEnumerable<OrderSummaryModel>> GetCurrentAccountOrders()
    {
        var orders = await repositories.Orders.GetAccountOrdersAsync(currentAccount.Id);

        return orders
            .OrderByDescending(order => order.CreatedAt)
            .Select(OrdersMapper.ToOrderSummaryModel);
    }

    public async Task<OrderDetailedModel> FindCurrentAccountOrder(string id)
    {
        var order = await repositories.Orders
            .FindOrderAsync(currentAccount.Id, id)
            .EnsureIsNotNull(id);

        return order.Map(OrdersMapper.ToOrderDetailedModel);
    }
}