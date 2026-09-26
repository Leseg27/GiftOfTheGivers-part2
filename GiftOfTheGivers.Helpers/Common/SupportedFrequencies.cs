namespace GiftOfTheGivers.Helpers.Common;

/// <summary>
/// Central list of supported donation frequencies.
/// </summary>
public static class SupportedFrequencies
{
    public static readonly IReadOnlyList<string> All = new[]
    {
        "OnceOff", "Monthly", "Quarterly", "Annually"
    };

    public static bool IsSupported(string? frequency)
        => !string.IsNullOrWhiteSpace(frequency)
           && All.Contains(frequency, StringComparer.OrdinalIgnoreCase);
}
