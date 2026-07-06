using Application.Repositories.Abstractions;
using Application.Services.Abstractions;
using Contracts.DTO;

namespace Application.Services;

public class TemperatureService : ITemperatureService
{
    private readonly ISensorRepository _sensorRepository;

    public TemperatureService(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
    }

    public List<TemperatureValueDTO> GetTemperature()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return new List<TemperatureValueDTO>
        {
            new TemperatureValueDTO
            {
                SensorId = 0,
                Value = _sensorRepository.GetSensorValue(0),
                Timestamp = timestamp
            },
            new TemperatureValueDTO
            {
                SensorId = 1,
                Value = _sensorRepository.GetSensorValue(1),
                Timestamp = timestamp
            },
            new TemperatureValueDTO
            {
                SensorId = 2,
                Value = _sensorRepository.GetSensorValue(2),
                Timestamp = timestamp
            }
        };
    }
}
