namespace GiftOfTheGivers.Helpers.Formatting;

/// <summary>
/// Generates dummy tax-certificate numbers for donations.
/// Format: GOTG-YYYY-XXXXXX where XXXXXX is the zero-padded donation Id.
/// </summary>
public static class TaxCertificateFormatter
{
    /// <summary>
    /// Generates a tax certificate number for the given donation Id.
    /// </summary>
    /// <param name="donationId">The unique donation Id (must be greater than zero).</param>
    /// <param name="year">Optional year. Defaults to the current UTC year.</param>
    /// <returns>A string like "GOTG-2026-000123".</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="donationId"/> is zero or negative.
    /// </exception>
    public static string Generate(int donationId, int? year = null)
    {
        if (donationId <= 0)
            throw new ArgumentOutOfRangeException(nameof(donationId),
                "Donation Id must be greater than zero.");

        var y = year ?? DateTime.UtcNow.Year;
        return $"GOTG-{y}-{donationId:D6}";
    }
}
