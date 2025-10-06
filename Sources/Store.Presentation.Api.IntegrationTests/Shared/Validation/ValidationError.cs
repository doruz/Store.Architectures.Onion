namespace Store.Presentation.Api.IntegrationTests;

public record ValidationError(string Property, params string[] Messages)
{
    public KeyValuePair<string, string[]> ToKeyValuePair() => new(Property, Messages);
}