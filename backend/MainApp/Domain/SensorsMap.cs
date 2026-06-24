using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Index(nameof(Type), nameof(SensorId), IsUnique = true)]
[Index(nameof(Id), IsUnique = true)]
public class SensorsMap
{
    public Guid Id { get; set; }
    public SensorType Type { get; set; }
    public long SensorId { get; set; }
    public string SensorName { get; set; }
    public ICollection<SensorValue> SensorValues { get; set; } = new List<SensorValue>();
}
