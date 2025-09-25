public sealed record ShoppingCartLine(string ProductId)
{
    public int Quantity { get; set; } = 1;
}