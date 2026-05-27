using SmartDoorSensor.Enums;

namespace SmartDoorSensor.DTO;

public class DoorStatusDTO
{
    public DoorStatusType Status { get; set; }
    public long Timestamp { get; set; }
}
