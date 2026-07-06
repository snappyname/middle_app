using Application.Repositories.Abstract;

namespace Application.Repositories;

public class SensorRepository : ISensorRepository
{
    private const int MAX_ACCURACY = 3;
    private const float MINIMAL_VALUE = 0.2f;

    public float GetSensorValue(long sensorId)
    {
        return MathF.Round(Random.Shared.NextSingle() * (1f - MINIMAL_VALUE) + MINIMAL_VALUE, MAX_ACCURACY);
    }
}
