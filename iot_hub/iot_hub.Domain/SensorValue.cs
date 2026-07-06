using Domain.Enums;

namespace Domain;

public abstract class SensorValue
{
    public abstract SensorType SensorType { get; }
    public long SensorId { get; set; }
    public long Timestamp { get; set; }
}

public abstract class SensorValue<T> : SensorValue
{
    public T Value { get; set; } = default!;
}
