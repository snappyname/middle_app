using Domain.Enums;

namespace Domain;

public abstract class SensorValue
{
    public SensorType SensorType { get; init; }
    public long Timestamp { get; init; }
}

public abstract class SensorValue<T> : SensorValue
{
    public T Value { get; init; } = default!;
}
