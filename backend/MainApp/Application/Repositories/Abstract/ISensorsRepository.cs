using Domain;
using Domain.Enums;

namespace Application.Repositories.Abstract;

public interface ISensorsRepository
{
    Task<List<SensorsMap>> GetSensorsMappingAsync(CancellationToken cancellationToken = default);
    Task AddNewSensorsMappingAsync(List<SensorsMap> sensorsMaps, CancellationToken cancellationToken = default);
    Task AddNewSensorsValuesAsync(List<SensorValue> values, CancellationToken cancellationToken = default);
    Task<bool> IsSensorExist(SensorType sensorType, long sensorId, CancellationToken cancellationToken = default);
    Task<Guid> AddNewSensor(SensorType sensorType, long sensorId, string sensorName, CancellationToken cancellationToken = default);
    Task RenameSensorAsync(Guid mappedSensorId, string sensorName, CancellationToken cancellationToken = default);
    Task<List<SensorValue>> GetSensorsValuesAsync(Guid mappedSensorId, long startTime, long endTime, int count, CancellationToken cancellationToken = default);
}
