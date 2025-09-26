using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.Products;

public record ProductWriteModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; init; }
}