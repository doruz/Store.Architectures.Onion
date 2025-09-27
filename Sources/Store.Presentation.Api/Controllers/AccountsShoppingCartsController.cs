using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("accounts/current/shopping-cart")]
public sealed class AccountsShoppingCartsController(ShoppingCartsService shoppingCarts) : BaseApiController
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
    public async Task<IActionResult> UpdateCurrentCart([FromBody] ShoppingCartLineWriteModel[] lines)
    {
        await shoppingCarts.UpdateCurrentAccountCart(lines);

        return NoContent();
    }

    /// <summary>
    /// Removed product from cart of authenticated account.
    /// </summary>
    /// <param name="productId">identifier of product to be removed.</param>
    [HttpDelete("products/{productId}")]
    public async Task<IActionResult> RemoveProductFromCurrentAccountCart([FromRoute] string productId)
    {
        await shoppingCarts.RemoveProductFromCurrentAccountCart(productId);

        return NoContent();
    }
}