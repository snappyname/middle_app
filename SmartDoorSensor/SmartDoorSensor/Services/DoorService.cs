using SmartDoorSensor.DTO;
using SmartDoorSensor.Repository.Abstract;
using SmartDoorSensor.Services.Abstractions;

namespace SmartDoorSensor.Services;

public class DoorService : IDoorService
{
    private readonly IDoorRepository _doorRepository;

    public DoorService(IDoorRepository doorRepository)
    {
        _doorRepository = doorRepository;
    }

    public async Task<DoorStatusDTO> GetStatus()
    {
        return new DoorStatusDTO
        {
            Status = await _doorRepository.GetDoorStatus(), Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
    }

    public async Task SetStatus(SetDoorStatusDTO newStatus)
    {
        await _doorRepository.SetDoorStatus(newStatus.DoorStatus);
    }
}
