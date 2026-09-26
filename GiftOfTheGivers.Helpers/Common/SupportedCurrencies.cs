namespace GiftOfTheGivers.Helpers.Common;

/// <summary>
/// Central list of supported currency codes for the platform.
/// </summary>
public static class SupportedCurrencies
{
    public static readonly IReadOnlyList<string> All = new[] { "ZAR", "USD", "EUR" };

    public static bool IsSupported(string? currency)
        => !string.IsNullOrWhiteSpace(currency)
           && All.Contains(currency.ToUpperInvariant());
}
