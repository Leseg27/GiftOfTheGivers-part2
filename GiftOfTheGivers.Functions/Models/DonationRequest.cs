namespace GiftOfTheGivers.Functions.Models;

/// <summary>
/// Payload accepted by the ProcessDonation HTTP trigger.
/// </summary>
public class DonationRequest
{
    public string? DonorName { get; set; }
    public string? DonorEmail { get; set; }
    public bool IsAnonymous { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public string? Frequency { get; set; }
}
