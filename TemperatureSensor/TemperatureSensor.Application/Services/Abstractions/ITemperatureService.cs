using Contracts.DTO;

namespace Application.Services.Abstractions;

public interface ITemperatureService
{
    List<TemperatureValueDTO> GetTemperature();
}
