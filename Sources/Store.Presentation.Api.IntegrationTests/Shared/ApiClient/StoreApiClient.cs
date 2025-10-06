namespace Store.Presentation.Api.IntegrationTests;

public sealed class StoreApiClient(HttpClient httpClient) : IDisposable
{
    public AdminApiClient Admin { get; } = new(httpClient);

    public void Dispose() => httpClient.Dispose();
}