using Microsoft.Extensions.DependencyInjection;
using Store.Core.Business.Products;
using Store.Core.Business.ShoppingCarts;

namespace Store.Core.Business;

public static class BusinessLayer
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        return services
            .AddSingleton<ShoppingCartsService>()
            .AddSingleton<ProductsService>();
    }
}