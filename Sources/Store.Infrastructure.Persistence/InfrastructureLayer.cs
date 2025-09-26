global using Store.Core.Shared;

using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence;

public static class InfrastructureLayer
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProductsRepository, ProductsInMemoryRepository>()
            .AddSingleton<IShoppingCartsRepository, ShoppingCartsInMemoryRepository>()
            .AddSingleton<RepositoriesContext>();
    }
}