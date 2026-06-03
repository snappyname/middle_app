using Coravel;
using iot_hub.BackgroundJobs;

namespace iot_hub.Extentions;

public static class ApplicationBuilderExtensions
{
    public static void UseApplicationSchedulers(this WebApplication app)
    {
        app.Services.UseScheduler(scheduler =>
        {
            scheduler.ScheduleAsync(async () =>
            {
                await app.Services.GetRequiredService<GetDoorDataJob>().Invoke();
                await app.Services.GetRequiredService<GetTemperatureDataJob>().Invoke();
                await app.Services.GetRequiredService<GetHumidityDataJob>().Invoke();
            }).EverySeconds(5);
        });
    }
}
