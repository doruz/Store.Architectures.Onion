using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class HttpResponseAssertions(HttpResponseMessage response)
{
    public AndConstraint<HttpResponseAssertions> HaveStatusCode(HttpStatusCode statusCode)
    {
        response.StatusCode.Should().Be(statusCode);

        return new AndConstraint<HttpResponseAssertions>(this);
    }

    public AndConstraint<ObjectAssertions> ContentBeEquivalentTo<T>(object expectedValue)
    {
        var responseContent = response.Content.ReadFromJsonAsync<T>().Result;

        return responseContent.Should().BeEquivalentTo(expectedValue);
    }

    public AndConstraint<GenericCollectionAssertions<T>> ContentBeEquivalentTo<T>(params IEnumerable<object> expectedValues)
    {
        var responseContent = response.Content.ReadFromJsonAsync<T[]>().Result;

        return responseContent.Should().BeEquivalentTo(expectedValues);
    }

    public AndConstraint<HttpResponseAssertions> ContainValidationError(ValidationError validationError)
        => ContainValidationError(validationError.ToKeyValuePair());

    private AndConstraint<HttpResponseAssertions> ContainValidationError(KeyValuePair<string, string[]> expectedError)
    {
        var responseContent = response.Content.ReadFromJsonAsync<ValidationProblemDetails>().Result;

        responseContent
            .Should()
            .NotBeNull();

        responseContent.Errors
            .Should()
            .ContainKey(expectedError.Key);

        responseContent.Errors.First(e => e.Key == expectedError.Key)
            .Should()
            .BeEquivalentTo(expectedError);

        return new AndConstraint<HttpResponseAssertions>(this);
    }

    public AndConstraint<StringAssertions> ContentContainId()
    {
        var responseContent = response.Content.ReadFromJsonAsync<EntityId>().Result;

        responseContent.Should().NotBeNull();

        return responseContent.Id.Should()
            .NotBeNullOrEmpty()
            .And.NotBeNullOrWhiteSpace();
    }
}