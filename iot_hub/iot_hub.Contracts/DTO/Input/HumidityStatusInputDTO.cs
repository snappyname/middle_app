namespace Contracts.DTO.Input
{
    public class HumidityStatusInputDTO
    {
        public long SensorId { get; set; }
        public decimal Value { get; set; }
        public long Timestamp { get; set; }
    }
}
