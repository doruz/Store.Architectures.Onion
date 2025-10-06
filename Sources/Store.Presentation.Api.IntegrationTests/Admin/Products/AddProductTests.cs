using Store.Core.Business.Products;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class AddProductTests(StoreApiFactory factory) : StoreApiBaseTests(factory)
{
    [Theory]
    [ClassData(typeof(AddProductValidationData))]
    public async Task When_ProductDetailsAreInvalid_Should_ReturnValidationErrors(AddProductTestModel invalidProduct, ValidationError expectedError)
    {
        // Act
        var response = await Api.Admin.AddProductAsync(invalidProduct);

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.BadRequest)
            .And.ContainValidationError(expectedError);
    }

    [Fact]
    public async Task When_ProductDetailsAreValid_Should_ReturnAddedProductDetails()
    {
        // Arrange
        var newProduct = AddProductTestModel.CreateRandomDetails();

        // Act
        var response = await Api.Admin.AddProductAsync(newProduct);

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.Created)
            .And.ContentBeEquivalentTo<ProductModel>(newProduct.GetExpectedDetails());
    }

    [Fact]
    public async Task When_ProductDetailsAreValid_Should_ReturnAddedProductId()
    {
        // Arrange
        var newProduct = AddProductTestModel.CreateRandomDetails();

        // Act
        var response = await Api.Admin.AddProductAsync(newProduct);

        // Assert
        response.Should().ContentContainId();
    }
}
