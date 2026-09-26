using GiftOfTheGivers.Helpers.Donations;

namespace GiftOfTheGivers.Tests.Helpers;

public class DonationCalculatorTests
{
    [Theory]
    [InlineData(1,    true)]
    [InlineData(100,  true)]
    [InlineData(0,    false)]
    [InlineData(-1,   false)]
    [InlineData(999_999_999,   true)]
    [InlineData(1_000_000_000, false)]
    public void IsValidAmount_Works(decimal amount, bool expected)
    {
        Assert.Equal(expected, DonationCalculator.IsValidAmount(amount));
    }

    [Theory]
    [InlineData(100, "OnceOff",   100)]
    [InlineData(100, "Monthly",   1200)]
    [InlineData(100, "Quarterly", 400)]
    [InlineData(100, "Annually",  100)]
    [InlineData(100, "monthly",   1200)]
    public void AnnualisedTotal_Works(decimal amount, string frequency, decimal expected)
    {
        Assert.Equal(expected, DonationCalculator.AnnualisedTotal(amount, frequency));
    }

    [Fact]
    public void AnnualisedTotal_Throws_OnInvalidFrequency()
    {
        Assert.Throws<ArgumentException>(
            () => DonationCalculator.AnnualisedTotal(100, "Weekly"));
    }

    [Fact]
    public void AnnualisedTotal_Throws_OnZeroAmount()
    {
        Assert.Throws<ArgumentException>(
            () => DonationCalculator.AnnualisedTotal(0, "Monthly"));
    }
}
