using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class SensorValue
{
    public Guid Id { get; set; }
    public Guid SensorsMapId { get; set; }
    public string Value { get; set; }
    public long Timestamp { get; set; }

    public SensorsMap SensorsMap { get; set; } = null!;
}
