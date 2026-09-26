using GiftOfTheGivers.Functions.Models;
using GiftOfTheGivers.Helpers.Common;
using GiftOfTheGivers.Helpers.Donations;
using GiftOfTheGivers.Helpers.Formatting;
using Microsoft.Extensions.Logging;

namespace GiftOfTheGivers.Functions.Services;

/// <summary>
/// Default implementation that validates the request and returns a
/// structured response. Uses GiftOfTheGivers.Helpers for the business rules.
/// </summary>
public class DonationProcessingService : IDonationProcessingService
{
    private readonly ILogger<DonationProcessingService> _logger;

    public DonationProcessingService(ILogger<DonationProcessingService> logger)
    {
        _logger = logger;
    }

    public DonationResponse Process(DonationRequest request)
    {
        var errors = new List<string>();

        // ── Validation ─────────────────────────────────────────
        if (!DonationCalculator.IsValidAmount(request.Amount))
            errors.Add("Amount must be greater than zero and within the allowed range.");

        if (!SupportedCurrencies.IsSupported(request.Currency))
            errors.Add("Currency must be one of: ZAR, USD, EUR.");

        if (!SupportedFrequencies.IsSupported(request.Frequency))
            errors.Add("Frequency must be one of: OnceOff, Monthly, Quarterly, Annually.");

        if (!request.IsAnonymous && string.IsNullOrWhiteSpace(request.DonorName))
            errors.Add("Donor name is required unless the donation is anonymous.");

        if (errors.Count > 0)
        {
            _logger.LogWarning("Donation validation failed: {Errors}", string.Join("; ", errors));
            return new DonationResponse
            {
                Success = false,
                Message = "Validation failed.",
                Errors = errors
            };
        }

        // ── Business logic (helpers) ───────────────────────────
        var cert = TaxCertificateFormatter.Generate(
            donationId: Math.Abs(Guid.NewGuid().GetHashCode() % 900_000) + 100_000);

        var formatted = CurrencyFormatter.Format(request.Amount, request.Currency!);
        var annualised = DonationCalculator.AnnualisedTotal(request.Amount, request.Frequency!);

        _logger.LogInformation(
            "Donation processed: {FormattedAmount} ({Frequency}) Annualised={Annualised} Cert={Cert}",
            formatted, request.Frequency, annualised, cert);

        return new DonationResponse
        {
            Success = true,
            TaxCertificateNumber = cert,
            FormattedAmount = formatted,
            AnnualisedTotal = annualised,
            Message = "Donation processed successfully."
        };
    }
}
