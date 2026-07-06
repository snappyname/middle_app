using Application.Repositories.Abstractions;

namespace Application.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly Random _random;

    public SensorRepository()
    {
        _random = new Random();
    }
    
    public int GetSensorValue(int sensorId)
    {
        return _random.Next(15, 32);
    }
}
