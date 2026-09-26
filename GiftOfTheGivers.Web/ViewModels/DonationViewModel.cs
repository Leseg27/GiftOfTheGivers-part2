using System.ComponentModel.DataAnnotations;
using GiftOfTheGivers.Web.Models;

namespace GiftOfTheGivers.Web.ViewModels;

public class DonationViewModel
{
    [MaxLength(100)]
    [Display(Name = "Full Name")]
    public string? DonorName { get; set; }

    [EmailAddress, MaxLength(150)]
    [Display(Name = "Email Address")]
    public string? DonorEmail { get; set; }

    [Display(Name = "Donate anonymously")]
    public bool IsAnonymous { get; set; }

    [Required]
    [Range(0.01, 999_999_999, ErrorMessage = "Please enter an amount greater than zero.")]
    [Display(Name = "Donation Amount")]
    public decimal Amount { get; set; }

    [Required]
    [Display(Name = "Currency")]
    public DonationCurrency Currency { get; set; } = DonationCurrency.ZAR;

    [Required]
    [Display(Name = "Frequency")]
    public DonationFrequency Frequency { get; set; } = DonationFrequency.OnceOff;
}
