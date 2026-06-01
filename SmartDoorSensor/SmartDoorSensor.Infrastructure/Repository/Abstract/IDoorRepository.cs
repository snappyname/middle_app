using Domain.Enums;

namespace Infrastructure.Repository.Abstract;

public interface IDoorRepository
{
    Task<DoorStatusType> GetDoorStatus();
    Task SetDoorStatus(DoorStatusType newStatus);
}
