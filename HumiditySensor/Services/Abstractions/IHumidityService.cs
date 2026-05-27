using HumiditySensor.DTO;

namespace HumiditySensor.Services.Abstractions;

public interface IHumidityService
{
    HumidityValueDTO GetHumidity();
}