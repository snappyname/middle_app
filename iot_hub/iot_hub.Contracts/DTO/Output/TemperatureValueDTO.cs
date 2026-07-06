namespace Contracts.DTO
{
    public class TemperatureValueDTO
    {
        public long SensorId { get; set; }
        public int Value { get; set; }
        public string Timestamp { get; set; }
    }
}
