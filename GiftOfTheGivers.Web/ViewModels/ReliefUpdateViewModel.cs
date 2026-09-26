using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Web.ViewModels;

public class ReliefUpdateViewModel
{
    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Project (optional)")]
    public int? ReliefProjectId { get; set; }
}
