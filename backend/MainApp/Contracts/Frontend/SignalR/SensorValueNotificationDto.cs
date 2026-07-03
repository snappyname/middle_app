using Domain.Enums;

namespace Contracts.Frontend.SignalR;

public class SensorValueNotificationDto
{
    public Guid SensorMapId { get; set; }
    public long SensorId { get; set; }
    public SensorType SensorType { get; set; }
    public string SensorName { get; set; }
    public string Value { get; set; }
    public long Timestamp { get; set; }
}
