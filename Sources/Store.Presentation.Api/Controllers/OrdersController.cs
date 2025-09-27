using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Orders;

[ApiRoute("accounts/current/orders")]
public sealed class OrdersController(OrdersService orders) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetOrdersSummary()
        => Ok(await orders.GetCurrentAccountOrders());

    [HttpGet("{orderId}")]
    public async Task<IActionResult> FindOrderDetails([FromRoute] string orderId)
        => OkOrNotFound(await orders.FindCurrentAccountOrder(orderId));
}