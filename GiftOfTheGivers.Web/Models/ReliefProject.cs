using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Web.Models;

public class ReliefProject
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Category { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public ICollection<ReliefUpdate> Updates { get; set; } = new List<ReliefUpdate>();
}