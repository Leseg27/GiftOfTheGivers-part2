using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Web.Services;

public class VolunteerService : IVolunteerService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<VolunteerService> _logger;

    public VolunteerService(ApplicationDbContext db, ILogger<VolunteerService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Volunteer> CreateAsync(Volunteer volunteer)
    {
        _db.Volunteers.Add(volunteer);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Volunteer {Id} registered: {Name}", volunteer.Id, volunteer.FullName);
        return volunteer;
    }

    public async Task<IEnumerable<Volunteer>> GetAllAsync()
        => await _db.Volunteers.OrderByDescending(v => v.SubmittedAt).ToListAsync();
}