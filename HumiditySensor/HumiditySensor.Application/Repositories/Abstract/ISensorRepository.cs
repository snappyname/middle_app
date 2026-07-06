namespace Application.Repositories.Abstract;

public interface ISensorRepository
{
    float GetSensorValue(long sensorId);
}
