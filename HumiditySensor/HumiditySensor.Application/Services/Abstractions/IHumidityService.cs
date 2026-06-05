using Contracts.DTOs;

namespace Application.Services.Abstractions;

public interface IHumidityService
{
    HumidityValueDTO GetHumidity();
}
