namespace GiftOfTheGivers.Helpers.Donations;

/// <summary>
/// Validation and calculation helpers for donations.
/// </summary>
public static class DonationCalculator
{
    /// <summary>Upper bound to protect against fat-finger entries.</summary>
    public const decimal MaxAmount = 999_999_999m;

    /// <summary>
    /// Returns true when the amount is strictly positive and within bounds.
    /// </summary>
    public static bool IsValidAmount(decimal amount)
        => amount > 0m && amount <= MaxAmount;

    /// <summary>
    /// Converts a donation amount into its annualised value based on frequency.
    /// </summary>
    /// <param name="amount">The per-instalment amount.</param>
    /// <param name="frequency">OnceOff, Monthly, Quarterly or Annually (case-insensitive).</param>
    /// <returns>The annualised total.</returns>
    /// <exception cref="ArgumentException">Thrown for unsupported frequency values.</exception>
    public static decimal AnnualisedTotal(decimal amount, string frequency)
    {
        if (!IsValidAmount(amount))
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        return (frequency ?? string.Empty).ToLowerInvariant() switch
        {
            "onceoff" or "once-off" or "one-time" => amount,
            "monthly"                             => amount * 12m,
            "quarterly"                           => amount * 4m,
            "annually" or "annual" or "yearly"    => amount,
            _ => throw new ArgumentException(
                    $"Unsupported frequency '{frequency}'.", nameof(frequency))
        };
    }
}
