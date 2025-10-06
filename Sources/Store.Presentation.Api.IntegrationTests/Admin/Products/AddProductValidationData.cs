namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal sealed class AddProductValidationData : ValidationTheoryData<AddProductTestModel>
{
    protected override AddProductTestModel ValidModel { get; } = AddProductTestModel.CreateRandomDetails();

    public AddProductValidationData()
    {
        Add(ValidModel with { Name = "" }, "Name", "Field is required.");
        Add(ValidModel with { Name = TestRandom.Strings.Generate(101) }, "Name", "Value must have maximum length 100.");
        Add(ValidModel with { Price = TestRandom.Numbers.Generate(int.MinValue, 0) }, "Price", "Value must be greater or equal than 0.");
        Add(ValidModel with { Stock = TestRandom.Numbers.Generate(int.MinValue, 0) }, "Stock", "Value must be greater or equal than 0.");
    }
}