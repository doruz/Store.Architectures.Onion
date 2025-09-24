using System.ComponentModel.DataAnnotations;

namespace Store.Business.Products;

public record ProductWriteModel
{
    [Required] [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [Range(0, double.MaxValue)]
    public double Price { get; init; }
}