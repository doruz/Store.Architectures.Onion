using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Errors;
using Store.Core.Business.Orders;
using Store.Core.Business.ShoppingCarts;

// TODO: to rename from accounts to customers

[ApiRoute("accounts/current/shopping-carts/current")]
public sealed class AccountsShoppingCartsController(
    ShoppingCartsService shoppingCarts,
    ShoppingCartCheckoutService shoppingCartCheckout) : BaseApiController
{
    /// <summary>
    /// Get current cart of authenticated account.
    /// If there is no cart created, create an empty one.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ShoppingCartModel>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentAccountCart());

    /// <summary>
    /// Clear cart of authenticated account.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
   
    public async Task<IActionResult> ClearCurrentCart()
    {
        await shoppingCarts.ClearCurrentAccountCart();

        return NoContent();
    }

    /// <summary>
    /// Update cart lines of authenticated account.
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] EditShoppingCartLineModel[] lines)
    {
        await shoppingCarts.UpdateCurrentAccountCart(lines);

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
        OrderSummaryModel orderSummary = await shoppingCartCheckout.CheckoutCurrentAccountCart();

        return CreatedAtRoute("OrderDetails", new { OrderId = orderSummary.Id }, orderSummary);
    }
}