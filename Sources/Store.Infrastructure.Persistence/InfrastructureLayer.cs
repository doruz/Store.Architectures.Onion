global using Store.Core.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;
using Store.Infrastructure.Persistence.Cosmos;
using Store.Infrastructure.Persistence.InMemory;

namespace Store.Infrastructure.Persistence;

public static class InfrastructureLayer
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.UseCosmos())
        {
            services.AddCosmosPersistence(configuration);
        }
        else
        {
            services.AddInMemoryPersistence();
        }

        return services.AddSingleton<RepositoriesContext>();
    }

    internal static bool UseCosmos(this IConfiguration configuration)
        => configuration["AllowedPersistence"].IsEqualTo("cosmos");
}