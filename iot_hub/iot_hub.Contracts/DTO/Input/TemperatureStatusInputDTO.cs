namespace Contracts.DTO.Input
{
    public class TemperatureStatusInputDTO
    {
        public long SensorId { get; set; }
        public int Value { get; set; }
        public long Timestamp { get; set; }
    }
}
