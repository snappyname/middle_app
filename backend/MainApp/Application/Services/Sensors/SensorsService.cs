using Application.Repositories.Abstract;
using Application.Services.Abstract.Sensors;
using Contracts.Frontend.Sensors;
using Contracts.Kafka;
using Domain;
using Mapster;

namespace Application.Services.Sensors;

public class SensorsService : ISensorsService
{
    private readonly ISensorsRepository _sensorsRepository;

    public SensorsService(ISensorsRepository sensorsRepository)
    {
        _sensorsRepository = sensorsRepository;
    }

    public async Task LogSensorsValuesAsync(List<BaseSensorValueKafkaDTO> sensorValues)
    {
        await ValidateSensorsAsync(sensorValues);
        var mappedItems = await _sensorsRepository.GetSensorsMappingAsync();
        var newItems = new List<SensorValue>();
        foreach (var sensorValue in sensorValues)
        {
            var item = new SensorValue
            {
                Value = sensorValue.Value,
                Timestamp = long.Parse(sensorValue.Timestamp),
                SensorsMapId = mappedItems.First(x =>
                    x.SensorId == sensorValue.SensorId && x.Type == sensorValue.SensorType).Id
            };
            newItems.Add(item);
        }

        await _sensorsRepository.AddNewSensorsValuesAsync(newItems);
    }

    public async Task<List<SensorDTO>> GetAllSensorMapping()
    {
        var sensors = await _sensorsRepository.GetSensorsMappingAsync();
        return sensors.Adapt<List<SensorDTO>>();
    }

    public async Task RenameSensor(Guid id, string newName)
    {
        await _sensorsRepository.RenameSensor(id, newName);
    }

    private async Task ValidateSensorsAsync(List<BaseSensorValueKafkaDTO> sensorValues)
    {
        var mappedItems = await _sensorsRepository.GetSensorsMappingAsync();
        var newItems = new List<SensorsMap>();
        foreach (var sensorValue in sensorValues)
        {
            if (!mappedItems.Any(x => x.SensorId == sensorValue.SensorId && x.Type == sensorValue.SensorType))
            {
                newItems.Add(new SensorsMap
                {
                    SensorId = sensorValue.SensorId, Type = sensorValue.SensorType, SensorName = String.Empty
                });
            }
        }

        if (newItems.Any())
        {
            await _sensorsRepository.AddNewSensorsMappingAsync(newItems);
        }
    }
    
}
