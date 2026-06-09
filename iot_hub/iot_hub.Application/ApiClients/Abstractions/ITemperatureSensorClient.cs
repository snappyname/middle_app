using Contracts.DTO.Input;
using Refit;

namespace Application.ApiClients.Abstractions
{
    public interface ITemperatureSensorClient
    {
        [Get("/Temperature")]
        Task<List<TemperatureStatusInputDTO>> GetTemperature();
    }
}
