using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosDatabaseContainers(CosmosClient cosmosClient, IOptions<CosmosOptions> options)
{
    public const string ProductsName = "Products";
    public const string ShoppingCartsName = "ShoppingCarts";

    public Container Products => GetContainer(ProductsName);
    public Container ShoppingCarts => GetContainer(ShoppingCartsName);

    private Container GetContainer(string containerName)
        => cosmosClient.GetContainer(options.Value.DatabaseName, containerName);
}