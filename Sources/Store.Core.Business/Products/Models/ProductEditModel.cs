using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.Products;

public record ProductEditModel
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; init; }
}