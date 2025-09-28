using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.ShoppingCarts;
using Store.Core.Shared;

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
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] EditShoppingCartLineModel[] lines)
    {
        await shoppingCarts.UpdateCurrentAccountCart(lines);

        return NoContent();
    }

    [HttpPost("checkout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckoutCart()
    {
        await shoppingCartCheckout.CheckoutCurrentAccountCart();

        return NoContent();
    }
}