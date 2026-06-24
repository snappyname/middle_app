using Domain;
using Domain.Enums;

namespace Application.Repositories.Abstract
{
    public interface ISensorsRepository
    {
        Task<List<SensorsMap>> GetSensorsMappingAsync();
        Task AddNewSensorsMappingAsync(List<SensorsMap> sensorsMaps);
        Task AddNewSensorsValuesAsync(List<SensorValue> values);
        Task<bool> IsSensorExist(SensorType sensorType, long sensorId);
        Task<Guid> AddNewSensor(SensorType sensorType, long sensorId, string sensorName);
        Task RenameSensor(Guid mappedSensorId, string sensorName);
    }
}
