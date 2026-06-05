using Contracts.DTO.Input;
using Refit;

namespace Application.ApiClients.Abstractions
{
    public interface IHumiditySensorClient
    {
        [Get("/Humidity")]
        Task<HumidityStatusInputDTO> GetHumidity();
    }
}
