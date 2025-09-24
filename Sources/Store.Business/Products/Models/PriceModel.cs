using Store.Core.Domain;

namespace Store.Business.Products;

public record PriceModel(double Value, string Label)
{
    public static PriceModel Create(Price price) => new(price.Amount, price.ToString());
}