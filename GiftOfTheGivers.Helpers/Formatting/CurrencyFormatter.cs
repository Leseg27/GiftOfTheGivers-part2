using System.Globalization;

namespace GiftOfTheGivers.Helpers.Formatting;

/// <summary>
/// Formats monetary amounts according to the selected currency symbol.
/// Supports ZAR, USD and EUR.
/// </summary>
public static class CurrencyFormatter
{
    /// <summary>
    /// Formats a decimal amount with the currency symbol.
    /// </summary>
    /// <param name="amount">The amount to format.</param>
    /// <param name="currency">ZAR, USD or EUR (case-insensitive).</param>
    /// <returns>A formatted string, e.g. "R 100.00" or "$ 1,234.56".</returns>
    /// <exception cref="ArgumentException">Thrown for unsupported currency codes.</exception>
    public static string Format(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        return currency.ToUpperInvariant() switch
        {
            "ZAR" => "R " + amount.ToString("N2", CultureInfo.InvariantCulture),
            "USD" => "$ " + amount.ToString("N2", CultureInfo.InvariantCulture),
            "EUR" => "€ " + amount.ToString("N2", CultureInfo.InvariantCulture),
            _     => throw new ArgumentException(
                        $"Unsupported currency '{currency}'. Use ZAR, USD or EUR.", nameof(currency))
        };
    }
}
