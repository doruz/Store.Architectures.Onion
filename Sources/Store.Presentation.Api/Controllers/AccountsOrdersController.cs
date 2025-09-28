using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Orders;

[ApiRoute("accounts/current/orders")]
public sealed class AccountsOrdersController(OrdersService orders) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<OrderSummaryModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersSummary()
        => Ok(await orders.GetCurrentAccountOrders());

    [HttpGet("{orderId}")]
    [ProducesResponseType<OrderDetailedModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindOrderDetails([FromRoute] string orderId)
        => Ok(await orders.FindCurrentAccountOrder(orderId));
}