using FluentAssertions;
using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests.Entities;

public class ShoppingCartTests
{
    private static readonly ShoppingCartLine _cartLine = new("apples", 1);
    private static readonly ShoppingCartLine[] _cartLines =
    [
        new ShoppingCartLine("1", 1),
        new ShoppingCartLine("2", 2),
        new ShoppingCartLine("3", 3),
    ];

    private readonly ShoppingCart _systemUnderTest = ShoppingCart.CreateEmpty("1234");

    [Fact]
    public void When_ProductIsNew_Should_BeAdded()
    {
        // Act
        _systemUnderTest.UpdateOrRemoveLine(_cartLine);

        // Assert
        _systemUnderTest.Lines.Should().HaveCount(1);
        _systemUnderTest.Lines.Should().Contain(_cartLine);
    }

    [Fact]
    public void When_ProductExists_Should_BeUpdated()
    {
        // Arrange
        ShoppingCartLine updatedCartLine = _cartLine.IncreaseQuantity(2);

        // Act
        _systemUnderTest.UpdateOrRemoveLine(_cartLine);
        _systemUnderTest.UpdateOrRemoveLine(updatedCartLine);

        // Assert
        _systemUnderTest.Lines.Should().HaveCount(1);
        _systemUnderTest.Lines.Should().Contain(updatedCartLine);
    }

    [Fact]
    public void When_ProductQuantityIsZero_Should_BeRemoved()
    {
        // Arrange
        ShoppingCartLine newCartLine = new("1", 1);
        ShoppingCartLine updatedCartLine = new("1", 0);

        // Act
        _systemUnderTest.UpdateOrRemoveLine(newCartLine);
        _systemUnderTest.UpdateOrRemoveLine(updatedCartLine);

        // Assert
        _systemUnderTest.Lines.Should().BeEmpty();
    }

    [Fact]
    public void When_ProductsAreNew_Should_AllBeAdded()
    {
        // Act
        _systemUnderTest.UpdateOrRemoveLines(_cartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo(_cartLines);
    }

    [Fact]
    public void When_ProductsHaveZeroQuantity_Should_BeRemoved()
    {
        // Arrange
        ShoppingCartLine[] updatedCartLines =
        [
            _cartLines[0].IncreaseQuantity(3),
            _cartLines[1].IncreaseQuantity(5),
            _cartLines[2].WithQuantity(0)
        ];

        // Act
        _systemUnderTest.UpdateOrRemoveLines(updatedCartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo(updatedCartLines.Take(2));
    }

    [Fact]
    public void When_SameProductsAreUpdated_Should_BeMerged()
    {
        // Arrange
        ShoppingCartLine expectedCartLine = _cartLine.WithQuantity(5);

        ShoppingCartLine[] updatedCartLines =
        [
            _cartLine.WithQuantity(0),
            _cartLine.WithQuantity(2),
            _cartLine.WithQuantity(3),
        ];

        // Act
        _systemUnderTest.UpdateOrRemoveLines(updatedCartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo([expectedCartLine]);
    }
}