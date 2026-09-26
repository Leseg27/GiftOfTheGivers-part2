using GiftOfTheGivers.Helpers.Formatting;

namespace GiftOfTheGivers.Tests.Helpers;

public class TaxCertificateFormatterTests
{
    [Fact]
    public void Generate_ReturnsExpectedFormat()
    {
        var result = TaxCertificateFormatter.Generate(42, 2026);
        Assert.Equal("GOTG-2026-000042", result);
    }

    [Fact]
    public void Generate_PadsSingleDigits()
    {
        var result = TaxCertificateFormatter.Generate(7, 2026);
        Assert.Equal("GOTG-2026-000007", result);
    }

    [Fact]
    public void Generate_UsesCurrentYear_WhenYearNotProvided()
    {
        var result = TaxCertificateFormatter.Generate(1);
        Assert.StartsWith($"GOTG-{DateTime.UtcNow.Year}-", result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Generate_Throws_WhenIdNotPositive(int id)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => TaxCertificateFormatter.Generate(id));
    }
}
