using Coravel;
using iot_hub.BackgroundJobs;
using iot_hub.Configurations;
using Microsoft.Extensions.Options;

namespace iot_hub.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseApplicationSchedulers(this WebApplication app)
    {
        var settings = app.Services
            .GetRequiredService<IOptions<AppSettings>>()
            .Value;
        
        app.Services.UseScheduler(scheduler =>
        {
            scheduler.Schedule(async () =>
                {
                    await RunSensorJobs(app.Services);
                })
                .EverySeconds(settings.RequestsRateInSeconds)
                .PreventOverlapping("SensorJobs");
        });
    }
    
    static async Task RunSensorJobs(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        try
        {
            await scope.ServiceProvider.GetRequiredService<GetDoorDataJob>().Invoke();
            await scope.ServiceProvider.GetRequiredService<GetTemperatureDataJob>().Invoke();
            await scope.ServiceProvider.GetRequiredService<GetHumidityDataJob>().Invoke();
        }
        catch (Exception ex)
        {
            services.GetRequiredService<ILogger<Program>>()
                .LogError(ex, "Unhandled exception in sensor jobs");
        }
    }
}
