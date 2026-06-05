using Domain.Enums;

namespace Domain;

public class TemperatureValue : SensorValue<float>
{
    public override SensorType SensorType => SensorType.Temperature;
}
