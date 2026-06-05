using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Coravel.Invocable;
using Domain;
using Mapster;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public class GetTemperatureDataJob : IInvocable
{
    private readonly ITemperatureSensorClient _temperatureSensorsClient;
    private readonly IKafkaService _kafkaService;
    private readonly ILogger<GetHumidityDataJob> _logger;

    public GetTemperatureDataJob(ITemperatureSensorClient temperatureSensorsClient, IKafkaService kafkaService, ILogger<GetHumidityDataJob> logger)
    {
        _temperatureSensorsClient = temperatureSensorsClient;
        _kafkaService = kafkaService;
        _logger = logger;
    }

    public async Task Invoke()
    {
        var result = await _temperatureSensorsClient.GetTemperature();
        _logger.LogInformation($"GetTemperatureDataJob result: {JsonSerializer.Serialize(result)}");
        var sensorValue = result.Adapt<TemperatureValue>();
        await _kafkaService.SendNewValue(sensorValue);
    }
}
