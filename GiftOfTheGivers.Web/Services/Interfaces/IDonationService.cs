using GiftOfTheGivers.Web.Models;

namespace GiftOfTheGivers.Web.Services.Interfaces;

public interface IDonationService
{
    Task<Donation> CreateAsync(Donation donation, string? userId);
    Task<IEnumerable<Donation>> GetAllAsync();
    Task<Donation?> GetByIdAsync(int id);
}