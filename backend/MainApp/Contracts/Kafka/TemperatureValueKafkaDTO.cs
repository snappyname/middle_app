namespace Contracts.Kafka;

public class TemperatureValueKafkaDTO
{
    public long SensorId { get; set; }
    public int Value { get; set; }
    public string Timestamp { get; set; }
}
