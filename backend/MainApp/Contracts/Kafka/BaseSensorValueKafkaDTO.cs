using Domain.Enums;

namespace Contracts.Kafka;

public class BaseSensorValueKafkaDTO
{
    public int SensorId { get; set; }
    public SensorType SensorType { get; set; }
    public string Value { get; set; }
    public string Timestamp { get; set; }
}
