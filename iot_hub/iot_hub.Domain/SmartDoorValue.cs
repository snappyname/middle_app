using Domain.Enums;

namespace Domain;

public class SmartDoorValue : SensorValue<DoorState>
{
    public override SensorType SensorType => SensorType.SmartDoor;
}
