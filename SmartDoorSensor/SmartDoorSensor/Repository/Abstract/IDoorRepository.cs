using SmartDoorSensor.Enums;

namespace SmartDoorSensor.Repository.Abstract;

public interface IDoorRepository
{
    Task<DoorStatusType> GetDoorStatus();
    Task SetDoorStatus(DoorStatusType newStatus);
}
