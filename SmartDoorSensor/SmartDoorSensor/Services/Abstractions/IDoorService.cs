using SmartDoorSensor.DTO;

namespace SmartDoorSensor.Services.Abstractions;

public interface IDoorService
{
    Task<DoorStatusDTO> GetStatus();
    Task SetStatus(SetDoorStatusDTO newStatus);
}