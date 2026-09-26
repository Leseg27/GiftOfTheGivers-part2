using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Web.Models;

public class Volunteer
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string Skills { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Availability { get; set; } = string.Empty;

    [EmailAddress, MaxLength(150)]
    public string? Email { get; set; }

    [Phone, MaxLength(20)]
    public string? Phone { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}