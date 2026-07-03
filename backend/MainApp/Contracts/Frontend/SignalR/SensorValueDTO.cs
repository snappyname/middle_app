namespace Contracts.Frontend.SignalR;

public class SensorValueDTO
{
    public Guid SensorId { get; set; }
    public string Value { get; set; }
    public long Timestamp { get; set; }
}
