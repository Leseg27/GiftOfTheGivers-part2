using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Web.ViewModels;

public class VolunteerViewModel
{
    [Required, MaxLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    [Display(Name = "Skills")]
    public string Skills { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    [Display(Name = "Availability")]
    public string Availability { get; set; } = string.Empty;

    [EmailAddress, MaxLength(150)]
    [Display(Name = "Email Address")]
    public string? Email { get; set; }

    [Phone, MaxLength(20)]
    [Display(Name = "Phone Number")]
    public string? Phone { get; set; }
}
