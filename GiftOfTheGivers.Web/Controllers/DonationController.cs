using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using GiftOfTheGivers.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers.Web.Controllers;

public class DonationController : Controller
{
    private readonly IDonationService _donationService;
    private readonly ILogger<DonationController> _logger;

    public DonationController(IDonationService donationService,
                              ILogger<DonationController> logger)
    {
        _donationService = donationService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Create() => View(new DonationViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DonationViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        if (!vm.IsAnonymous && string.IsNullOrWhiteSpace(vm.DonorName))
        {
            ModelState.AddModelError(nameof(vm.DonorName),
                "Please provide your name or tick 'Donate anonymously'.");
            return View(vm);
        }

        var userId = User.Identity?.IsAuthenticated == true
            ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            : null;

        var donation = new Donation
        {
            DonorName = vm.IsAnonymous ? "Anonymous" : vm.DonorName,
            DonorEmail = vm.DonorEmail,
            IsAnonymous = vm.IsAnonymous,
            Amount = vm.Amount,
            Currency = vm.Currency,
            Frequency = vm.Frequency
        };

        try
        {
            var saved = await _donationService.CreateAsync(donation, userId);
            return RedirectToAction(nameof(Confirmation), new { id = saved.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing donation.");
            ModelState.AddModelError("", "Something went wrong. Please try again.");
            return View(vm);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var donation = await _donationService.GetByIdAsync(id);
        if (donation is null) return NotFound();
        return View(donation);
    }

    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Index()
        => View(await _donationService.GetAllAsync());
}