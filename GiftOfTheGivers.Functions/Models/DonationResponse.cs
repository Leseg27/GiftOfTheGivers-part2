namespace GiftOfTheGivers.Functions.Models;

/// <summary>
/// Structured response returned by the ProcessDonation HTTP trigger.
/// </summary>
public class DonationResponse
{
    public bool Success { get; set; }
    public string? TaxCertificateNumber { get; set; }
    public string? FormattedAmount { get; set; }
    public decimal AnnualisedTotal { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}
