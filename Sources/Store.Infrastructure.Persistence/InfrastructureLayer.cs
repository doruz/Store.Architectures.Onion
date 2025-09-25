using Microsoft.Extensions.DependencyInjection;
using Store.Core.Repositories;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure;

public static class InfrastructureLayer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProductsRepository, ProductsInMemoryRepository>()
            .AddSingleton<IShoppingCartsRepository, ShoppingCartsInMemoryRepository>()
            .AddSingleton<RepositoriesContext>();
    }
}