using GiftOfTheGivers.Functions.Models;

namespace GiftOfTheGivers.Functions.Services;

/// <summary>
/// Business logic for processing an incoming donation request.
/// Separated from HTTP handling so it can be unit-tested independently.
/// </summary>
public interface IDonationProcessingService
{
    DonationResponse Process(DonationRequest request);
}
