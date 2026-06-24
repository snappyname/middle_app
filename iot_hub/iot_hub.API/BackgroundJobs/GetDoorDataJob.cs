using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Domain;
using Mapster;

namespace iot_hub.BackgroundJobs;

public class GetDoorDataJob : SensorJob<SmartDootInputStateDTO>
{
    private readonly IDoorSensorsClient _doorSensorsClient;
    private readonly IKafkaService _kafkaService;

    public GetDoorDataJob(IKafkaService kafkaService, ILogger<GetDoorDataJob> logger, IDoorSensorsClient doorSensorsClient)
        : base(logger)
    {
        _doorSensorsClient = doorSensorsClient;
        _kafkaService = kafkaService;
    }

    protected override string JobName => nameof(GetDoorDataJob);
    
    protected override Task<SmartDootInputStateDTO> GetDataAsync()
    {
        return _doorSensorsClient.GetDoorStatus();
    }

    protected override async Task SendAsync(SmartDootInputStateDTO result)
    {
        var sensorValues = result.Adapt<SmartDoorValue>();
        await _kafkaService.SendNewValueAsync(sensorValues);
    }
}
