using Contracts.DTO;
using Contracts.DTO.Input;
using Refit;

namespace Application.ApiClients.Abstractions
{
    public interface IDoorSensorsClient
    {
        [Get("/SmartDoor")]
        Task<SmartDootInputStateDTO> GetDoorStatus(CancellationToken cancellationToken = default);
    }
}
