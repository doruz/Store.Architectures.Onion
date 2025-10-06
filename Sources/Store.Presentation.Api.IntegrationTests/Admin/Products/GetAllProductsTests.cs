using Store.Core.Business.Products;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

// DONE
public class GetAllProductsTests(StoreApiFactory factory) : StoreApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductsAreRetrieved_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProducts = new List<object>
        {
            new
            {
                Id = ProductsTestData.ApplesId,
                Name = "Apples",
                Price = new { Value = 0.75m, Currency = "€", Display = "€0.75" },
                Stock = 10
            },

            new
            {
                Id = ProductsTestData.BananasId,
                Name = "Bananas",
                Price = new { Value = 0.99m, Currency = "€", Display = "€0.99" },
                Stock = 5
            },

            new
            {
                Id = ProductsTestData.OrangesId,
                Name = "Oranges",
                Price = new { Value = 0.75m, Currency = "€", Display = "€0.75" },
                Stock = 0
            }
        };

        // Act
        var response = await Api.Admin.GetAllProductsAsync();

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContentBeEquivalentTo<ProductModel>(expectedProducts)
            .And.BeInAscendingOrder(p => p.Name);
    }
}