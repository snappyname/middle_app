using Application.Services.Abstractions;
using Contracts.DTOs;

namespace Application.Services;

public class HumidityService : IHumidityService
{
    private const int MAX_ACCURACY = 3;
    private const float MINIMAL_VALUE = 0.2f;

    public HumidityValueDTO GetHumidity()
    {
        return new HumidityValueDTO
        {
            Value = MathF.Round(Random.Shared.NextSingle() * (1f - MINIMAL_VALUE) + MINIMAL_VALUE, MAX_ACCURACY),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
    }
}
