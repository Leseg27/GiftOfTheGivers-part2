using GiftOfTheGivers.Functions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddScoped<IDonationProcessingService, DonationProcessingService>();
    })
    .Build();

host.Run();
