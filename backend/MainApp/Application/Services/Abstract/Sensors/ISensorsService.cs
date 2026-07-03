using Contracts.Frontend.Sensors;
using Contracts.Kafka;
using Domain;

namespace Application.Services.Abstract.Sensors;

public interface ISensorsService
{
    Task LogSensorsValuesAsync(List<BaseSensorValueKafkaDTO> sensorValues, CancellationToken cancellationToken = default);
    Task<List<SensorDTO>> GetAllSensorMapping(CancellationToken cancellationToken = default);
    Task RenameSensor(Guid id, string newName = "", CancellationToken cancellationToken = default);
    Task<List<SensorValue>> GetSensorsValuesAsync(Guid mappedSensorId, long startTime, long endTime, int count, CancellationToken cancellationToken = default);
}
