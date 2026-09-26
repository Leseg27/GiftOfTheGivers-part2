using GiftOfTheGivers.Helpers.Formatting;
using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Web.Services;

public class DonationService : IDonationService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DonationService> _logger;

    public DonationService(ApplicationDbContext db, ILogger<DonationService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Donation> CreateAsync(Donation donation, string? userId)
    {
        if (donation.Amount <= 0)
            throw new ArgumentException("Donation amount must be greater than zero.", nameof(donation));

        if (!donation.IsAnonymous && !string.IsNullOrEmpty(userId))
            donation.UserId = userId;

        _db.Donations.Add(donation);
        await _db.SaveChangesAsync();

        // Using the shared helper from GiftOfTheGivers.Helpers
        donation.TaxCertificateNumber = TaxCertificateFormatter.Generate(donation.Id);
        await _db.SaveChangesAsync();

        var formatted = CurrencyFormatter.Format(donation.Amount, donation.Currency.ToString());

        _logger.LogInformation(
            "Donation {Id} recorded: {FormattedAmount} ({Frequency}) Anonymous={Anon} Cert={Cert}",
            donation.Id, formatted, donation.Frequency, donation.IsAnonymous,
            donation.TaxCertificateNumber);

        return donation;
    }

    public async Task<IEnumerable<Donation>> GetAllAsync()
        => await _db.Donations.OrderByDescending(d => d.CreatedAt).ToListAsync();

    public Task<Donation?> GetByIdAsync(int id)
        => _db.Donations.FirstOrDefaultAsync(d => d.Id == id);
}
