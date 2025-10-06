using Store.Core.Business.Products;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class EditProductTests(StoreApiFactory factory) : StoreApiBaseTests(factory)
{
    [Theory]
    [ClassData(typeof(EditProductValidationData))]
    public async Task When_ProductDetailsAreInvalid_Should_ReturnValidationErrors(EditProductTestModel product, ValidationError expectedError)
    {
        // Act
        var response = await Api.Admin.EditProductAsync(ProductsTestData.UnknownId, product);

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.BadRequest)
            .And.ContainValidationError(expectedError);
    }

    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.EditProductAsync(ProductsTestData.UnknownId, new EditProductTestModel());

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnNoContent()
    {
        // Act
        var response = await Api.Admin.EditProductAsync(ProductsTestData.ApplesId, new EditProductTestModel());

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task When_()
    {
        // Arrange
        var expectedSameProductDetails = new
        {
            Id = ProductsTestData.ApplesId,
            Name = "Apples",
            Price = new { Value = 0.75m, Currency = "€", Display = "€0.75" },
            Stock = 10
        };

        // Act
        await Api.Admin
            .EditProductAsync(ProductsTestData.ApplesId, new EditProductTestModel())
            .EnsureIsSuccess();

        // Assert
        var productDetails = await Api.Admin.FindProductAsync(ProductsTestData.ApplesId);

        productDetails
            .Should()
            .ContentBeEquivalentTo<ProductModel>(expectedSameProductDetails);
    }

    [Fact]
    public async Task When__Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.EditProductAsync(ProductsTestData.ApplesId, new EditProductTestModel() {Name = "Red Apples"});

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }
}