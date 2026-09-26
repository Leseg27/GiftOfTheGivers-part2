using System.Net;
using System.Text.Json;
using GiftOfTheGivers.Functions.Models;
using GiftOfTheGivers.Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GiftOfTheGivers.Functions.Functions;

/// <summary>
/// HTTP-triggered function that processes donation requests.
/// Endpoint: POST /api/donations/process
/// </summary>
public class DonationFunction
{
    private readonly IDonationProcessingService _service;
    private readonly ILogger<DonationFunction> _logger;

    public DonationFunction(IDonationProcessingService service,
                            ILogger<DonationFunction> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Function("ProcessDonation")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "donations/process")]
        HttpRequest req)
    {
        _logger.LogInformation("ProcessDonation invoked at {Time}", DateTime.UtcNow);

        // ── Parse body ────────────────────────────────────────
        DonationRequest? request;
        try
        {
            request = await JsonSerializer.DeserializeAsync<DonationRequest>(
                req.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Malformed JSON payload.");
            return new BadRequestObjectResult(new DonationResponse
            {
                Success = false,
                Message = "Malformed JSON payload."
            });
        }

        if (request is null)
        {
            _logger.LogWarning("Empty request body.");
            return new BadRequestObjectResult(new DonationResponse
            {
                Success = false,
                Message = "Request body is required."
            });
        }

        // ── Process with error handling ───────────────────────
        try
        {
            var response = _service.Process(request);

            if (!response.Success)
                return new BadRequestObjectResult(response);

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while processing donation.");

            return new ObjectResult(new DonationResponse
            {
                Success = false,
                Message = "An unexpected error occurred. Please try again later."
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
