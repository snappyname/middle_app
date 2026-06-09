using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Coravel.Invocable;
using Domain;
using Mapster;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public class GetDoorDataJob : IInvocable
{
    private readonly IDoorSensorsClient _doorSensorsClient;
    private readonly IKafkaService _kafkaService;
    private readonly ILogger<GetDoorDataJob> _logger;
    public GetDoorDataJob(IKafkaService kafkaService, IDoorSensorsClient doorSensorsClient, ILogger<GetDoorDataJob> logger)
    {
        _kafkaService = kafkaService;
        _doorSensorsClient = doorSensorsClient;
        _logger = logger;
    }

    public async Task Invoke()
    {
        var result = await _doorSensorsClient.GetDoorStatus();
        _logger.LogInformation($"GetDoorDataJob result: {JsonSerializer.Serialize(result)}");
        var sensorValue = result.Adapt<SmartDoorValue>();
        await _kafkaService.SendNewValueAsync(sensorValue);
    }
}
