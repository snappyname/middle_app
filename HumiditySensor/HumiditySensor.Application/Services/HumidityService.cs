using Application.Services.Abstractions;
using Contracts.DTOs;

namespace Application.Services;

public class HumidityService : IHumidityService
{
    private const int MAX_ACCURACY = 3;
    private const float MINIMAL_VALUE = 0.2f;

    public List<HumidityValueDTO> GetHumidity()
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return new List<HumidityValueDTO>
        {
            new HumidityValueDTO
            {
                SensorId = 0,
                Value = MathF.Round(Random.Shared.NextSingle() * (1f - MINIMAL_VALUE) + MINIMAL_VALUE, MAX_ACCURACY),
                Timestamp = timeStamp,
            },
            new HumidityValueDTO
            {
                SensorId = 1,
                Value = MathF.Round(Random.Shared.NextSingle() * (1f - MINIMAL_VALUE) + MINIMAL_VALUE, MAX_ACCURACY),
                Timestamp = timeStamp,
            },
            new HumidityValueDTO
            {
                SensorId = 2,
                Value = MathF.Round(Random.Shared.NextSingle() * (1f - MINIMAL_VALUE) + MINIMAL_VALUE, MAX_ACCURACY),
                Timestamp = timeStamp,
            },
        };
    }
}
