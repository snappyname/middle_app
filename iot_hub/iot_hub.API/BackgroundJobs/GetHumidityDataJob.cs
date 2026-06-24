using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Coravel.Invocable;
using Domain;
using Mapster;
using System.Text.Json;
namespace iot_hub.BackgroundJobs;

public class GetHumidityDataJob : SensorJob<List<HumidityStatusInputDTO>>
{
    private readonly IHumiditySensorClient _humiditySensorClient;
    private readonly IKafkaService _kafkaService;

    public GetHumidityDataJob(IHumiditySensorClient humiditySensorClient, IKafkaService kafkaService, ILogger<GetHumidityDataJob> logger)
        : base(logger)
    {
        _humiditySensorClient = humiditySensorClient;
        _kafkaService = kafkaService;
    }

    protected override string JobName => nameof(GetHumidityDataJob);

    protected override Task<List<HumidityStatusInputDTO>> GetDataAsync()
    {
        return _humiditySensorClient.GetHumidity();
    }

    protected override async Task SendAsync(List<HumidityStatusInputDTO> result)
    {
        var sensorValues = result.Adapt<List<HumidityValue>>();
        await _kafkaService.SendNewValueAsync(sensorValues);
    }
}
