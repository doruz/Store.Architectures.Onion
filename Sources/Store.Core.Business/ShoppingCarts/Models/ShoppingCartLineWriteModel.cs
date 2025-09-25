using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.ShoppingCarts;

public record ShoppingCartLineWriteModel
{
    [Required]
    public required string ProductId { get; init; }

    [Range(0, 10)]
    public required int Quantity { get; init; }
}