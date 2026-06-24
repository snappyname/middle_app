using Contracts.Frontend.Sensors;
using Contracts.Kafka;

namespace Application.Services.Abstract.Sensors;

public interface ISensorsService
{
    Task LogSensorsValuesAsync(List<BaseSensorValueKafkaDTO> sensorValues);
    Task<List<SensorDTO>> GetAllSensorMapping();
    Task RenameSensor(Guid id, string newName);
}
