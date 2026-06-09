namespace Contracts.DTOs;

public class HumidityValueDTO
{
    public long SensorId { get; set; }
    public float Value { get; set; }
    public long Timestamp { get; set; }
}
