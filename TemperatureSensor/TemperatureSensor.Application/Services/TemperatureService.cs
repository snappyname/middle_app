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

    public TemperatureValueDTO GetTemperature()
    {
        return new TemperatureValueDTO
        {
            Value = _random.Next(15, 32), Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
    }
}
