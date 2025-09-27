using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.ShoppingCarts;

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
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentAccountCart());

    /// <summary>
    /// Clear cart of authenticated account.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> ClearCurrentCart()
    {
        await shoppingCarts.ClearCurrentAccountCart();

        return NoContent();
    }

    /// <summary>
    /// Update cart lines of authenticated account.
    /// </summary>
    [HttpPatch]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] ShoppingCartLineEditModel[] lines)
    {
        await shoppingCarts.UpdateCurrentAccountCart(lines);

        return NoContent();
    }

    // TODO: in case cart is empty should return 404
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutCart()
    {
        await shoppingCartCheckout.CheckoutCurrentAccountCart();

        return NoContent();
    }
}