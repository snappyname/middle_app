using Domain.Enums;

namespace Contracts.Frontend.Sensors;

public class SensorDTO
{
    public Guid MappedSensorId { get; set; }
    public SensorType SensorType { get; set; }
    public long SensorId { get; set; }
    public string? SensorName { get; set; }
}
