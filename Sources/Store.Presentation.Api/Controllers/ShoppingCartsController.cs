using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("shopping-carts")]
public class ShoppingCartsController(ShoppingCartsService shoppingCarts) : BaseApiController
{
    /// <summary>
    /// Get current cart of authenticated account.
    /// If there is no cart created, create an empty one.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentAccountCart());
}