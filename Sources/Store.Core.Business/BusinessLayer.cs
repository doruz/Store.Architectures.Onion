global using Store.Core.Business.Shared;

using Microsoft.Extensions.DependencyInjection;
using Store.Core.Business.Orders;
using Store.Core.Business.Products;
using Store.Core.Business.ShoppingCarts;

namespace Store.Core.Business;

public static class BusinessLayer
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        return services
            .AddScoped<ShoppingCartsService>()
            .AddScoped<ProductsService>()
            .AddScoped<OrdersService>()
            .AddScoped<ShoppingCartCheckoutService>();
    }
}