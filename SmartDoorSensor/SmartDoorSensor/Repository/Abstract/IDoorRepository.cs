using SmartDoorSensor.Enums;

namespace SmartDoorSensor.Repository.Abstract;

public interface IDoorRepository
{
    public Task<DoorStatusType> GetDoorStatus();
    public Task SetDoorStatus(DoorStatusType newStatus);
}