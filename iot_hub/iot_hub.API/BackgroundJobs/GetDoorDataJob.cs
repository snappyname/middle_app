using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Coravel.Invocable;
using Domain;
using Domain.Enums;
using Mapster;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public class GetDoorDataJob : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ISensorsClient _sensorsClient;
    private readonly ILogger<GetDoorDataJob> _logger;
    private readonly IKafkaService _kafkaService;

    public GetDoorDataJob(IHttpClientFactory factory, ILogger<GetDoorDataJob> logger, IKafkaService kafkaService, ISensorsClient sensorsClient)
    {
        _httpClient = factory.CreateClient();
        _logger = logger;
        _kafkaService = kafkaService;
        _sensorsClient = sensorsClient;
    }

    public async Task Invoke()
    {
        var result = await _sensorsClient.GetDoorStatus();
        var sensorValue = result.Adapt<SmartDoorValue>();
        sensorValue.SensorType = SensorType.SmartDoor;
        await _kafkaService.SendNewValue(sensorValue);
    }
}
