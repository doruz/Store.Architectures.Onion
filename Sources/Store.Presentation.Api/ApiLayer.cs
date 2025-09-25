using Store.Core.Business;
using Store.Core.Business.Products;
using Store.Core.Business.ShoppingCarts;
using Store.Infrastructure;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

namespace Store.Presentation.Api;

public static class ApiLayer
{
    public static IServiceCollection AddCurrentProject(this IServiceCollection services)
    {
        return services
            .AddBusiness()
            .AddPersistence()
            .AddTransient<ICurrentAccount, CurrentAccount>();
    }
}