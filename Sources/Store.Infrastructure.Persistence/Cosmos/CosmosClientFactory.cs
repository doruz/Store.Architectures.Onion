using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace Store.Infrastructure.Persistence.Cosmos;

internal static class CosmosClientFactory
{
    public static CosmosClient Create(IConfiguration configuration)
    {
        return new CosmosClientBuilder(configuration.GetConnectionString("Cosmos"))
            .WithContentResponseOnWrite(false)
            .WithSerializerOptions(new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                IgnoreNullValues = false
            })
            .BuildAndInitializeAsync([])
            .Result;
    }
}