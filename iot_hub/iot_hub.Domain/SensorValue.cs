using Domain.Enums;

namespace Domain;

public abstract class SensorValue
{
    public SensorType SensorType { get; set; }
    public long Timestamp { get; set; }
}

public abstract class SensorValue<T> : SensorValue
{
    public T Value { get; set; } = default!;
}
