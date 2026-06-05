using Domain.Enums;

namespace Domain;

public class HumidityValue : SensorValue<decimal>
{
    public override SensorType SensorType => SensorType.Humidity;
}
