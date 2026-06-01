using Domain.Enums;

namespace Contracts.DTO;

public class DoorStatusDTO
{
    public DoorStatusType Status { get; set; }
    public long Timestamp { get; set; }
}
