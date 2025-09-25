using Microsoft.AspNetCore.Mvc;
using Store.Business.ShoppingCarts;

[ApiRoute("shopping-carts")]
public class ShoppingCartsController(ShoppingCartsService shoppingCarts) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentAccountCart());
}