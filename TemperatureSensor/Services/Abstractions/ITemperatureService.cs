using TemperatureSensor.DTO;

namespace TemperatureSensor.Services.Abstractions;

public interface ITemperatureService
{
    TemperatureValueDTO GetTemperature();
}
