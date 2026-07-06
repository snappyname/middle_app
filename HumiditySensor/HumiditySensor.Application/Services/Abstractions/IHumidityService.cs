using Contracts.DTOs;

namespace Application.Services.Abstractions;

public interface IHumidityService
{
    List<HumidityValueDTO> GetHumidity();
}
