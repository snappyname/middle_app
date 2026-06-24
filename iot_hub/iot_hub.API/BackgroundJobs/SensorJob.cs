using Coravel.Invocable;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public abstract class SensorJob<TResponse> : IInvocable
{
    protected readonly ILogger Logger;

    protected SensorJob(ILogger logger)
    {
        Logger = logger;
    }

    protected abstract string JobName { get; }
    protected abstract Task<TResponse> GetDataAsync();
    protected abstract Task SendAsync(TResponse result);

    public async Task Invoke()
    {
        var result = await GetDataAsync();
        Logger.LogInformation("{JobName} result: {Result}", JobName, JsonSerializer.Serialize(result));
        await SendAsync(result);
    }
}

