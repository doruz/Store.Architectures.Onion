namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal sealed class EditProductValidationData : ValidationTheoryData<object>
{
    protected override object ValidModel { get; } = new();

    public EditProductValidationData()
    {
        Add(
            new { Name = TestRandom.Strings.Generate(101) },
            "Name", "" +
                    "Value must have maximum length 100."
        );

        Add(
            new { Price = TestRandom.Numbers.Generate(int.MinValue, 0) },
            "Price", "" +
                     "Value must be greater or equal than 0."
        );

        Add(
            new { Stock = TestRandom.Numbers.Generate(int.MinValue, 0) },
            "Stock",
            "Value must be greater or equal than 0."
        );
    }
}