using Store.Core.Business.Products;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

// DONE
public class FindProductTests(StoreApiFactory factory) : StoreApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.FindProductAsync(ProductsTestData.UnknownId);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProduct = new
        {
            Id = ProductsTestData.ApplesId,
            Name = "Apples",
            Price = new { Value = 0.75m, Currency = "€", Display = "€0.75" },
            Stock = 10
        };

        // Act
        var response = await Api.Admin.FindProductAsync(expectedProduct.Id);

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContentBeEquivalentTo<ProductModel>(expectedProduct);
    }
}