using TemperatureSensor.Services.Abstractions;

namespace TemperatureSensor.DTO;

public class TemperatureValueDTO
{
    public int Value {get; set;}
    public long Timestamp {get; set;}
}