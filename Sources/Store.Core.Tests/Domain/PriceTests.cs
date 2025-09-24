using FluentAssertions;

namespace Store.Core.Domain.Tests
{
    public class PriceTests
    {
        [Fact]
        public void When_PriceValueIsNegative_Should_ThrowException()
        {
            // Arrange & Act
            Action result = () => new Price(-1, "EUR");

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void When_CurrencyIsEmpty_Should_ThrowException()
        {
            // Arrange & Act
            Action result = () => new Price(0, "");

            // Assert
            result.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0, "EUR")]
        [InlineData(10, "EUR")]
        [InlineData(99.99, "EUR")]
        [InlineData(double.MaxValue, "EUR")]
        public void When_PriceIsCreated_Should_ContainCorrectData(double value, string currency)
        {
            // Arrange & Act
            var price = new Price(value, currency);

            // Assert
            price.Amount.Should().Be(value);
            price.Currency.Should().Be(currency);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1.5, 2, 3)]
        [InlineData(9.99, 2, 19.98)]
        [InlineData(9.99, 5, 49.95)]
        public void When_Price(double value, int quantity, double expectedValue)
        {
            // Arrange
            var price = Price.Euro(value);

            // Act
            var result = price * quantity;

            // Assert
            result.Amount.Should().Be(expectedValue);
            result.Currency.Should().Be(price.Currency);
        }
    }
}
