namespace Contracts.Kafka;

public class HumidityValueKafkaDTO
{
    public long SensorId { get; set; }
    public float Value { get; set; }
    public string Timestamp { get; set; }
}
