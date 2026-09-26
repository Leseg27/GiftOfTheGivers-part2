using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using GiftOfTheGivers.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GiftOfTheGivers.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly IReliefUpdateService _updates;

    public HomeController(ILogger<HomeController> logger,
                          ApplicationDbContext db,
                          IReliefUpdateService updates)
    {
        _logger = logger;
        _db = db;
        _updates = updates;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new HomeViewModel
        {
            TotalDonations = await _db.Donations.CountAsync(),
            TotalRaised = await _db.Donations.AnyAsync()
                ? await _db.Donations.SumAsync(d => d.Amount)
                : 0m,
            ActiveVolunteers = await _db.Volunteers.CountAsync(),
            ActiveProjects = await _db.ReliefProjects.CountAsync(p => p.IsActive)
        };
        ViewBag.LatestUpdates = (await _updates.GetAllAsync()).Take(3).ToList();
        return View(vm);
    }

    public IActionResult About() => View();
    public IActionResult Contact() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}