namespace Store.Core.Domain
{
    public sealed class Product : BaseEntity
    {
        public required string Name { get; set; }

        public required Price Price { get; set; }
    }
}