using Contracts.Enums;

namespace Contracts.DTO
{
    public class SmartDoorValueDTO
    {
        public DoorStatus Value { get; set; }
        public string Timestamp { get; set; }
    }
}
