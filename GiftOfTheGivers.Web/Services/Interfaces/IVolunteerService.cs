using GiftOfTheGivers.Web.Models;

namespace GiftOfTheGivers.Web.Services.Interfaces;

public interface IVolunteerService
{
    Task<Volunteer> CreateAsync(Volunteer volunteer);
    Task<IEnumerable<Volunteer>> GetAllAsync();
}