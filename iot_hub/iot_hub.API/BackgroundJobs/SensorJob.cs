using Coravel.Invocable;
using iot_hub.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public abstract class SensorJob<TResponse> : IInvocable
{
    private readonly int _cancelTokenTimeoutInSeconds;
    protected readonly ILogger Logger;

    protected SensorJob(ILogger logger, IOptions<AppSettings> appSettings)
    {
        _cancelTokenTimeoutInSeconds = appSettings.Value.RequestsRateInSeconds - 1;
        Logger = logger;
    }

    protected abstract string JobName { get; }
    protected abstract Task<TResponse> GetDataAsync(CancellationToken cancellationToken = default);
    protected abstract Task SendAsync(TResponse result);

    public async Task Invoke()
    {
        using var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenTimeoutInSeconds));
        try
        {
            var result = await GetDataAsync(cancellationToken.Token);
            Logger.LogInformation("{JobName} result: {Result}", JobName, JsonSerializer.Serialize(result));
            await SendAsync(result);
        }
        catch (OperationCanceledException)
        {
            Logger.LogWarning("{JobName} timed out", JobName);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{JobName} failed", JobName);
        }
    }
}

