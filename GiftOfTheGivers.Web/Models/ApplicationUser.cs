using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers.Web.Models;

/// <summary>
/// Application user extended from ASP.NET Core Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    public ICollection<ReliefUpdate> ReliefUpdates { get; set; } = new List<ReliefUpdate>();
}