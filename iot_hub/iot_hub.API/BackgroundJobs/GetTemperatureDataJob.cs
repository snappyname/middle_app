using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Domain;
using Mapster;

namespace iot_hub.BackgroundJobs;

public class GetTemperatureDataJob : SensorJob<List<TemperatureStatusInputDTO>>
{
    private readonly ITemperatureSensorClient _temperatureSensorClient;
    private readonly IKafkaService _kafkaService;
    
    public GetTemperatureDataJob(ITemperatureSensorClient client, IKafkaService kafkaService, ILogger<GetTemperatureDataJob> logger)
        : base(logger)
    {
        _temperatureSensorClient = client;
        _kafkaService = kafkaService;
    }
    
    protected override string JobName => nameof(GetTemperatureDataJob);

    protected override Task<List<TemperatureStatusInputDTO>> GetDataAsync()
    {
        return _temperatureSensorClient.GetTemperature();
    }

    protected override async Task SendAsync(List<TemperatureStatusInputDTO> result)
    {
        var sensorValues = result.Adapt<List<TemperatureValue>>();
        await _kafkaService.SendNewValueAsync(sensorValues);
    }
}
