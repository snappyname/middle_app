using Application.Repositories.Abstract;
using Application.Services.Abstractions;
using Contracts.DTOs;

namespace Application.Services;

public class HumidityService : IHumidityService
{
    private readonly ISensorRepository _sensorRepository;

    public HumidityService(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
    }

    public List<HumidityValueDTO> GetHumidity()
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return new List<HumidityValueDTO>
        {
            new HumidityValueDTO
            {
                SensorId = 0,
                Value = _sensorRepository.GetSensorValue(0),
                Timestamp = timeStamp,
            },
            new HumidityValueDTO
            {
                SensorId = 1,
                Value = _sensorRepository.GetSensorValue(1),
                Timestamp = timeStamp,
            },
            new HumidityValueDTO
            {
                SensorId = 2,
                Value = _sensorRepository.GetSensorValue(2),
                Timestamp = timeStamp,
            },
        };
    }
}
