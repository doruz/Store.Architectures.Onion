using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

internal static class HttpClientExtensions
{
    public static async Task<List<HttpResponseMessage>> ExecuteMultiple(
        this HttpClient httpClient,
        Func<HttpClient, Task<HttpResponseMessage>> action,
        int times)
    {
        var responses = new List<HttpResponseMessage>();

        for (int i = 0; i < times; i++)
        {
            responses.Add(await action(httpClient));
        }

        return responses;
    }

    public static async Task<T> ContentAs<T>(this Task<HttpResponseMessage> response)
        => await (await response).Content.ReadFromJsonAsync<T>();

    public static async Task<HttpResponseMessage> EnsureIsSuccess(this Task<HttpResponseMessage> response) 
        => (await response).EnsureSuccessStatusCode();
}