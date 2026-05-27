using Coravel;
using SmartDoorSensor.BackgroundJobs;

namespace SmartDoorSensor.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseApplicationSchedulers(this WebApplication app)
    {
        app.Services.UseScheduler(scheduler =>
        {
            scheduler.ScheduleAsync(async () =>
                {
                    await app.Services.GetRequiredService<RandomWebhookJob>().Invoke();
                }).EverySecond();
        });
    }
}