using GiftOfTheGivers.Helpers.Formatting;

namespace GiftOfTheGivers.Tests.Helpers;

public class CurrencyFormatterTests
{
    [Theory]
    [InlineData(100, "ZAR", "R 100.00")]
    [InlineData(100, "USD", "$ 100.00")]
    [InlineData(100, "EUR", "€ 100.00")]
    [InlineData(1234.5, "ZAR", "R 1,234.50")]
    [InlineData(0.99, "USD", "$ 0.99")]
    public void Format_ReturnsExpected(decimal amount, string currency, string expected)
    {
        Assert.Equal(expected, CurrencyFormatter.Format(amount, currency));
    }

    [Theory]
    [InlineData("zar", "R 5.00")]
    [InlineData("USD", "$ 5.00")]
    [InlineData("eur", "€ 5.00")]
    public void Format_IsCaseInsensitive(string currency, string expected)
    {
        Assert.Equal(expected, CurrencyFormatter.Format(5m, currency));
    }

    [Theory]
    [InlineData("GBP")]
    [InlineData("JPY")]
    [InlineData("")]
    public void Format_Throws_OnUnsupportedOrEmpty(string currency)
    {
        Assert.Throws<ArgumentException>(() => CurrencyFormatter.Format(10m, currency));
    }
}
