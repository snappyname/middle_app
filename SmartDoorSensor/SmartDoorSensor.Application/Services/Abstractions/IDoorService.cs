using Contracts.DTO;

namespace Application.Services.Abstractions;

public interface IDoorService
{
    Task<DoorStatusDTO> GetStatus();
    Task SetStatus(SetDoorStatusDTO newStatus);
}
