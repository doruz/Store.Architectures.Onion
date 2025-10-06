namespace Store.Presentation.Api.IntegrationTests;

[Collection("StoreApiTestsCollection")]
public abstract class StoreApiBaseTests(StoreApiFactory factory) : IAsyncLifetime
{
    private readonly CosmosTestDatabase _database = factory.GetService<CosmosTestDatabase>();

    protected StoreApiClient Api { get; } = new(factory.CreateDefaultClient());

    public async Task InitializeAsync()
    {
        await _database.EnsureIsCreated();
    }

    public async Task DisposeAsync()
    {
        Api.Dispose();

        await _database.EnsureIsDeleted();
        await factory.DisposeAsync();
    }
}