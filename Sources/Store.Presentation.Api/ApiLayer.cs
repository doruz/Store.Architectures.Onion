using Store.Api.Security;
using Store.Business;
using Store.Business.Products;
using Store.Business.ShoppingCarts;
using Store.Infrastructure;
using Store.Shared;

namespace Store.Api;

public static class ApiLayer
{
    public static IServiceCollection AddCurrentProject(this IServiceCollection services)
    {
        return services
            .AddBusiness()
            .AddInfrastructure()
            .AddTransient<ICurrentAccount, CurrentAccount>();
    }
}