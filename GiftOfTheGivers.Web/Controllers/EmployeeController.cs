using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using GiftOfTheGivers.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers.Web.Controllers;

[Authorize(Roles = "Employee")]
public class EmployeeController : Controller
{
    private readonly IReliefUpdateService _updates;
    private readonly IDonationService _donations;
    private readonly IVolunteerService _volunteers;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IReliefUpdateService updates,
                              IDonationService donations,
                              IVolunteerService volunteers,
                              ILogger<EmployeeController> logger)
    {
        _updates = updates;
        _donations = donations;
        _volunteers = volunteers;
        _logger = logger;
    }

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.DonationCount = (await _donations.GetAllAsync()).Count();
        ViewBag.VolunteerCount = (await _volunteers.GetAllAsync()).Count();
        var updates = await _updates.GetAllAsync();
        return View(updates);
    }

    [HttpGet]
    public async Task<IActionResult> CreateUpdate()
    {
        ViewBag.Projects = await _updates.GetActiveProjectsAsync();
        return View(new ReliefUpdateViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUpdate(ReliefUpdateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Projects = await _updates.GetActiveProjectsAsync();
            return View(vm);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var update = new ReliefUpdate
        {
            Title = vm.Title,
            Description = vm.Description,
            ReliefProjectId = vm.ReliefProjectId
        };

        await _updates.CreateAsync(update, userId);
        TempData["Success"] = "Relief update posted successfully.";
        return RedirectToAction(nameof(Dashboard));
    }
}