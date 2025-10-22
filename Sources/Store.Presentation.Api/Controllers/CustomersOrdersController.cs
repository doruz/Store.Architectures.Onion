using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Errors;
using Store.Core.Business.Orders;

[ApiRoute("customers/current/orders")]
public sealed class CustomersOrdersController(OrdersService orders) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<OrderSummaryModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersSummary()
        => Ok(await orders.GetCurrentCustomerOrders());

    [HttpGet("{orderId}", Name = "OrderDetails")]
    [ProducesResponseType<OrderDetailedModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindOrderDetails([FromRoute] string orderId)
        => Ok(await orders.FindCurrentCustomerOrder(orderId));
}