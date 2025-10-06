using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Persistence.Cosmos;
namespace Store.Presentation.Api.IntegrationTests;

public sealed class StoreApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public T GetService<T>()
        where T: notnull => Services.GetRequiredService<T>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            //.ConfigureAppConfiguration(ConfigureAppSettings)
            .ConfigureServices(services =>
            {
                services
                    .AddSingleton<CosmosDatabaseInitializer>()
                    .AddSingleton<CosmosTestDatabase>();
            })
            .UseEnvironment("development");
    }

    private static void ConfigureAppSettings(IConfigurationBuilder configuration)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.tests.json");

        configuration.AddJsonFile(appSettingsPath);
    }

    public async Task InitializeAsync()
    {
        Environment.SetEnvironmentVariable("CosmosOptions:DatabaseName", "Store-IntegrationTests");
    }

    public async Task DisposeAsync()
    {
    }
}