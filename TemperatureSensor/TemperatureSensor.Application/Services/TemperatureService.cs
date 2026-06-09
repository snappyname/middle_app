using Application.Services.Abstractions;
using Contracts.DTO;

namespace Application.Services;

public class TemperatureService : ITemperatureService
{
    private readonly Random _random;

    public TemperatureService()
    {
        _random = new Random();
    }

    public List<TemperatureValueDTO> GetTemperature()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return new List<TemperatureValueDTO>
        {
            new TemperatureValueDTO
            {
                SensorId = 0,
                Value = _random.Next(15, 32),
                Timestamp = timestamp
            },
            new TemperatureValueDTO
            {
                SensorId = 1,
                Value = _random.Next(15, 32),
                Timestamp = timestamp
            },
            new TemperatureValueDTO
            {
                SensorId = 2,
                Value = _random.Next(15, 32),
                Timestamp = timestamp
            }
        };
    }
}
