namespace Store.Presentation.Api.IntegrationTests;

[Collection("StoreApiTestsCollection")]
public abstract class StoreApiBaseTests(StoreApiFactory factory) : IAsyncLifetime
{
    private readonly TestCosmosDatabase _database = factory.GetService<TestCosmosDatabase>();

    protected StoreApiClient Api { get; } = new(factory.CreateDefaultClient());

    public async Task InitializeAsync()
    {
        await _database.EnsureIsCreated();
    }

    public async Task DisposeAsync()
    {
        await _database.EnsureIsDeleted();

        await factory.DisposeAsync();
        Api.Dispose();
    }
}