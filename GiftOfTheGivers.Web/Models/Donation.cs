using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftOfTheGivers.Web.Models;

public enum DonationFrequency
{
    OnceOff,
    Monthly,
    Quarterly,
    Annually
}

public enum DonationCurrency
{
    ZAR,
    USD,
    EUR
}

public class Donation
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? DonorName { get; set; }

    [EmailAddress, MaxLength(150)]
    public string? DonorEmail { get; set; }

    public bool IsAnonymous { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 999_999_999, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    public DonationCurrency Currency { get; set; } = DonationCurrency.ZAR;
    public DonationFrequency Frequency { get; set; } = DonationFrequency.OnceOff;

    [MaxLength(50)]
    public string? TaxCertificateNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}