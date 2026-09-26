using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Web.Services;

public class ReliefUpdateService : IReliefUpdateService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ReliefUpdateService> _logger;

    public ReliefUpdateService(ApplicationDbContext db, ILogger<ReliefUpdateService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ReliefUpdate> CreateAsync(ReliefUpdate update, string? userId)
    {
        update.PostedByUserId = userId;
        _db.ReliefUpdates.Add(update);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Relief update {Id} posted: {Title}", update.Id, update.Title);
        return update;
    }

    public async Task<IEnumerable<ReliefUpdate>> GetAllAsync()
        => await _db.ReliefUpdates
            .Include(u => u.ReliefProject)
            .Include(u => u.PostedBy)
            .OrderByDescending(u => u.PostedAt)
            .ToListAsync();

    public async Task<IEnumerable<ReliefProject>> GetActiveProjectsAsync()
        => await _db.ReliefProjects
            .Where(p => p.IsActive)
            .OrderBy(p => p.Title)
            .ToListAsync();
}