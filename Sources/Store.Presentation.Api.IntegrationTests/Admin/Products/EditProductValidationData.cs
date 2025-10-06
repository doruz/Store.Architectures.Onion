namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal sealed class EditProductValidationData : ValidationTheoryData<EditProductTestModel>
{
    protected override EditProductTestModel ValidModel { get; } = new();

    public EditProductValidationData()
    {
        Add(ValidModel with { Name = TestRandom.Strings.Generate(101) }, "Name", "Value must have maximum length 100.");
        Add(ValidModel with { Price = TestRandom.Numbers.Generate(int.MinValue, 0) }, "Price", "Value must be greater or equal than 0.");
        Add(ValidModel with { Stock = TestRandom.Numbers.Generate(int.MinValue, 0) }, "Stock", "Value must be greater or equal than 0.");
    }
}