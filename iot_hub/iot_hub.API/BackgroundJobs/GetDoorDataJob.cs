using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Domain;
using iot_hub.Configurations;
using Mapster;
using Microsoft.Extensions.Options;

namespace iot_hub.BackgroundJobs;

public class GetDoorDataJob : SensorJob<SmartDootInputStateDTO>
{
    private readonly IDoorSensorsClient _doorSensorsClient;
    private readonly IKafkaService _kafkaService;

    public GetDoorDataJob(IKafkaService kafkaService, ILogger<GetDoorDataJob> logger, IDoorSensorsClient doorSensorsClient, IOptions<AppSettings> appSettings)
        : base(logger, appSettings)
    {
        _doorSensorsClient = doorSensorsClient;
        _kafkaService = kafkaService;
    }

    protected override string JobName => nameof(GetDoorDataJob);

    protected override Task<SmartDootInputStateDTO> GetDataAsync(CancellationToken cancellationToken = default)
    {
         return _doorSensorsClient.GetDoorStatus(cancellationToken);
    }

    protected override async Task SendAsync(SmartDootInputStateDTO result)
    {
        var sensorValues = result.Adapt<SmartDoorValue>();
        await _kafkaService.SendNewValueAsync(sensorValues);
    }
}
