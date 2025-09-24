namespace Store.Core.Domain
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; set; } = default!;

        public Price Price { get; set; } = default!;

        // extend with quantity
    }
}