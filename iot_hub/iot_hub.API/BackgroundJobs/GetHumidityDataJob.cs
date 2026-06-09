using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Coravel.Invocable;
using Domain;
using Mapster;
using System.Text.Json;
namespace iot_hub.BackgroundJobs;

public class GetHumidityDataJob : IInvocable
{
    private readonly IHumiditySensorClient _humiditySensorClient;
    private readonly IKafkaService _kafkaService;
    private readonly ILogger<GetHumidityDataJob> _logger;
    
    public GetHumidityDataJob(IKafkaService kafkaService, IHumiditySensorClient humiditySensorClient, ILogger<GetHumidityDataJob> logger)
    {
        _kafkaService = kafkaService;
        _humiditySensorClient = humiditySensorClient;
        _logger = logger;
    }

    public async Task Invoke()
    {
        var result = await _humiditySensorClient.GetHumidity();
        _logger.LogInformation($"GetHumidityDataJob result: {JsonSerializer.Serialize(result)}");
        var sensorsValue = result.Adapt<List<HumidityValue>>();
        await _kafkaService.SendNewValueAsync(sensorsValue);
    }
}
