using Contracts.DTO;

namespace Application.Services.Abstractions;

public interface ITemperatureService
{
    TemperatureValueDTO GetTemperature();
}
