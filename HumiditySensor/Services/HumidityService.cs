using HumiditySensor.DTO;
using HumiditySensor.Services.Abstractions;

namespace HumiditySensor.Services;

public class HumidityService : IHumidityService
{
    public HumidityValueDTO GetHumidity()
    {
        return new HumidityValueDTO
        {
            Value = MathF.Round(Random.Shared.NextSingle() * 0.8f + 0.2f, 3),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
    }
}
