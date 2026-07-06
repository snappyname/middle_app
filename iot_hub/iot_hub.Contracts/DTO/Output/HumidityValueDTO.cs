namespace Contracts.DTO
{
    public class HumidityValueDTO
    {
        public long SensorId { get; set; }
        public float Value { get; set; }
        public string Timestamp { get; set; }
    }
}
