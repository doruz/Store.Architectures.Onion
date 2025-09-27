using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.Products;

public record EditProductModel
{
    [MaxLength(100)]
    public string? Name { get; init; }

    [Range(0, double.MaxValue)]
    public decimal? Price { get; init; }

    [Range(0, int.MaxValue)]
    public int? Stock { get; init; }
}