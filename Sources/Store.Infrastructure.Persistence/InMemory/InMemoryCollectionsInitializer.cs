using Store.Core.Domain.Entities;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryCollectionsInitializer(InMemoryDatabase database)
    : IAppInitializer
{
    public Task Execute()
    {
        database.Products.AddRange(GetProducts());

        return Task.CompletedTask;
    }

    private static List<Product> GetProducts() =>
    [
        new Product
        {
            Id = "9952a5da-9945-11f0-9ae5-2f38b5112d0d",
            Name = "Apple",
            Price = 2.5m,
            Stock = 10
        },
        new Product
        {
            Id = "dc552437-0b7d-455a-afa0-e4949142a4ad",
            Name = "Orange",
            Price = 1.99m,
            Stock = 10
        }
    ];
}