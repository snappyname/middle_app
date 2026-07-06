using Application.ApiClients.Abstractions;
using Application.Services.Abstract;
using Contracts.DTO.Input;
using Domain;
using iot_hub.Configurations;
using Mapster;
using Microsoft.Extensions.Options;

namespace iot_hub.BackgroundJobs;

public class GetHumidityDataJob : SensorJob<List<HumidityStatusInputDTO>>
{
    private readonly IHumiditySensorClient _humiditySensorClient;
    private readonly IKafkaService _kafkaService;

    public GetHumidityDataJob(IHumiditySensorClient humiditySensorClient, IKafkaService kafkaService, ILogger<GetHumidityDataJob> logger, IOptions<AppSettings> appSettings)
        : base(logger, appSettings)
    {
        _humiditySensorClient = humiditySensorClient;
        _kafkaService = kafkaService;
    }

    protected override string JobName => nameof(GetHumidityDataJob);

    protected override Task<List<HumidityStatusInputDTO>> GetDataAsync(CancellationToken cancellationToken = default)
    {
        return _humiditySensorClient.GetHumidity(cancellationToken);
    }

    protected override async Task SendAsync(List<HumidityStatusInputDTO> result)
    {
        var sensorValues = result.Adapt<List<HumidityValue>>();
        await _kafkaService.SendNewValueAsync(sensorValues);
    }
}
