using Store.Core.Domain.Entities;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryCollectionsInitializer(InMemoryDatabase database, ICurrentAccount currentAccount)
    : IAppInitializer
{
    public Task Execute()
    {
        database.Products.AddRange(GetProducts());
        database.ShoppingCarts.AddRange(GetShoppingCarts());
        database.Orders.AddRange(GetOrders());

        return Task.CompletedTask;
    }

    private List<Product> GetProducts() =>
    [
        new Product
        {
            Id = "9952a5da-9945-11f0-9ae5-2f38b5112d0d",
            Name = "Apple",
            Price = 2.5m
        },
        new Product
        {
            Id = "dc552437-0b7d-455a-afa0-e4949142a4ad",
            Name = "Orange",
            Price = 1.99m
        }
    ];

    private List<ShoppingCart> GetShoppingCarts() =>
    [
        new ShoppingCart
        {
            Id = currentAccount.Id,
            Lines =
            [
                new ShoppingCartLine("9952a5da-9945-11f0-9ae5-2f38b5112d0d", 3),
                new ShoppingCartLine("dc552437-0b7d-455a-afa0-e4949142a4ad", 2),
                new ShoppingCartLine("non-existing", 2)
            ]
        }
    ];

    private List<Order> GetOrders() =>
    [
        new Order
        {
            AccountId = currentAccount.Id,

            Lines =
            [
                new OrderLine
                {
                    ProductId = "9952a5da-9945-11f0-9ae5-2f38b5112d0d",
                    ProductName = "Apple",
                    ProductPrice = 2.5m,
                    Quantity = 2
                },

                new OrderLine
                {
                    ProductId = "dc552437-0b7d-455a-afa0-e4949142a4ad",
                    ProductName = "Orange",
                    ProductPrice = 1.99m,
                    Quantity = 3
                }
            ]
        }
    ];
}