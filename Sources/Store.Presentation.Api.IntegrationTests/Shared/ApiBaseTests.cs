using Store.Core.Shared;

namespace Store.Presentation.Api.IntegrationTests;

[Collection("ApiTestsCollection")]
public abstract class ApiBaseTests(ApiApplicationFactory factory) : IAsyncLifetime
{
    internal TestCosmosDatabase Database { get; } = factory.GetService<TestCosmosDatabase>();

    protected StoreApiClient Api { get; } = new(factory.CreateDefaultClient());

    protected ICurrentAccount CurrentAccount { get; } = factory.GetService<ICurrentAccount>();

    public virtual async Task InitializeAsync()
    {
        await Database.EnsureIsCreated();
    }

    public async Task DisposeAsync()
    {
    //    await _database.EnsureIsDeleted();

        await factory.DisposeAsync();
        Api.Dispose();
    }
}