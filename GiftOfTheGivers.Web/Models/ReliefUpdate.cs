using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Web.Models;

public class ReliefUpdate
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime PostedAt { get; set; } = DateTime.UtcNow;

    public int? ReliefProjectId { get; set; }
    public ReliefProject? ReliefProject { get; set; }

    public string? PostedByUserId { get; set; }
    public ApplicationUser? PostedBy { get; set; }
}