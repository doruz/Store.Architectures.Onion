using Microsoft.Extensions.DependencyInjection;
using Store.Business.Products;
using Store.Business.ShoppingCarts;

namespace Store.Business;

public static class BusinessLayer
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        return services
            .AddSingleton<ShoppingCartsService>()
            .AddSingleton<ProductsService>();
    }
}