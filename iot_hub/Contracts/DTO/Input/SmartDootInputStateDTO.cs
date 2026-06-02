using Contracts.Enums;

namespace Contracts.DTO.Input
{
    public class SmartDootInputStateDTO
    {
        public DoorStatus Status { get; set; }
        public long Timestamp { get; set; }
    }
}
