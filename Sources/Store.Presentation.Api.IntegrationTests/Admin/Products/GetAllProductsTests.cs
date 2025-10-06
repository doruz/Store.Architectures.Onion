using Store.Core.Business.Products;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class GetAllProductsTests(StoreApiFactory factory) : StoreApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductsAreRetrieved_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProducts = new List<ReadProductTestModel>
        {
            ReadProductTestModel.Create(ProductsTestData.Apples),
            ReadProductTestModel.Create(ProductsTestData.Bananas),
            ReadProductTestModel.Create(ProductsTestData.Oranges)
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