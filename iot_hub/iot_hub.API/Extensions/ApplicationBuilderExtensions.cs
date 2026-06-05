using Coravel;
using iot_hub.BackgroundJobs;

namespace iot_hub.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseApplicationSchedulers(this WebApplication app)
    {
        app.Services.UseScheduler(scheduler =>
        {
            scheduler.Schedule(() =>
                {
                    _ = app.Services.GetRequiredService<GetDoorDataJob>().Invoke();
                    _ = app.Services.GetRequiredService<GetTemperatureDataJob>().Invoke();
                    _ = app.Services.GetRequiredService<GetHumidityDataJob>().Invoke();
                })
                .EverySeconds(5);
        });
    }
}
