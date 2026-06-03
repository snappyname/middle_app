using Contracts.DTO.Input;
using Refit;

namespace Application.ApiClients.Abstractions
{
    public interface ISensorsClient
    {
        [Get("/SmartDoor")]
        Task<SmartDootInputStateDTO> GetDoorStatus();
    }
}
