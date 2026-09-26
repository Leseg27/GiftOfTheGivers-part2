using GiftOfTheGivers.Web.Models;

namespace GiftOfTheGivers.Web.Services.Interfaces;

public interface IReliefUpdateService
{
    Task<ReliefUpdate> CreateAsync(ReliefUpdate update, string? userId);
    Task<IEnumerable<ReliefUpdate>> GetAllAsync();
    Task<IEnumerable<ReliefProject>> GetActiveProjectsAsync();
}