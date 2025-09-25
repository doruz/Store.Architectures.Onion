using Store.Api.Security;
using Store.Core.Business;
using Store.Core.Business.Products;
using Store.Core.Business.ShoppingCarts;
using Store.Infrastructure;
using Store.Core.Shared;

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