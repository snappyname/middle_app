namespace Contracts.DTO;

public class TemperatureValueDTO
{
    public long SensorId { get; set; }
    public int Value { get; set; }
    public long Timestamp { get; set; }
}
