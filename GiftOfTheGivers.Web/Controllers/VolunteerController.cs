using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using GiftOfTheGivers.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers.Web.Controllers;

public class VolunteerController : Controller
{
    private readonly IVolunteerService _service;
    private readonly ILogger<VolunteerController> _logger;

    public VolunteerController(IVolunteerService service, ILogger<VolunteerController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Create() => View(new VolunteerViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VolunteerViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var volunteer = new Volunteer
        {
            FullName = vm.FullName,
            Skills = vm.Skills,
            Availability = vm.Availability,
            Email = vm.Email,
            Phone = vm.Phone
        };

        await _service.CreateAsync(volunteer);
        TempData["Success"] = "Thank you for registering as a volunteer!";
        return RedirectToAction(nameof(ThankYou));
    }

    [HttpGet]
    public IActionResult ThankYou() => View();

    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Index()
        => View(await _service.GetAllAsync());
}