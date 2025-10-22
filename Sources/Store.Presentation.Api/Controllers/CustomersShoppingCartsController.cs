using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Errors;
using Store.Core.Business.Orders;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("customers/current/shopping-carts/current")]
public sealed class CustomersShoppingCartsController(
    ShoppingCartsService shoppingCarts,
    ShoppingCartCheckoutService shoppingCartCheckout) : BaseApiController
{
    /// <summary>
    /// Get current cart of authenticated customer.
    /// If there is no cart created, create an empty one.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ShoppingCartModel>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentCustomerCart());

    /// <summary>
    /// Clear cart of authenticated customer.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
   
    public async Task<IActionResult> ClearCurrentCart()
    {
        await shoppingCarts.ClearCurrentCustomerCart();

        return NoContent();
    }

    /// <summary>
    /// Update cart lines of authenticated customer.
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] EditShoppingCartLineModel[] lines)
    {
        await shoppingCarts.UpdateCurrentCustomerCart(lines);

        return NoContent();
    }

    /// <summary>
    /// Make an order from current cart.
    /// </summary>
    [HttpPost("checkout")]
    [ProducesResponseType<OrderSummaryModel>(StatusCodes.Status201Created)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckoutCart()
    {
        OrderSummaryModel orderSummary = await shoppingCartCheckout.CheckoutCurrentCustomerCart();

        return CreatedAtRoute("OrderDetails", new { OrderId = orderSummary.Id }, orderSummary);
    }
}