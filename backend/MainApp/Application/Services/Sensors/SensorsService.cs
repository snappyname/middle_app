using Application.Repositories.Abstract;
using Application.Services.Abstract.Auth;
using Application.Services.Abstract.Sensors;
using Application.Services.Abstract.User;
using Application.SignalR;
using Contracts.Frontend.Sensors;
using Contracts.Frontend.SignalR;
using Contracts.Kafka;
using Domain;
using Mapster;

namespace Application.Services.Sensors;

public class SensorsService : ISensorsService
{
    private readonly ISensorsRepository _sensorsRepository;
    private readonly IRealtimeNotifier _realtimeNotifier;
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService; 

    public SensorsService(ISensorsRepository sensorsRepository, IRealtimeNotifier realtimeNotifier, IUserService userService, ICurrentUserService currentUserService)
    {
        _sensorsRepository = sensorsRepository;
        _realtimeNotifier = realtimeNotifier;
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task LogSensorsValuesAsync(List<BaseSensorValueKafkaDTO> sensorValues, CancellationToken cancellationToken)
    {
        await ValidateSensorsAsync(sensorValues, cancellationToken);
        var mappedItems = await _sensorsRepository.GetSensorsMappingAsync(cancellationToken);
        var newItems = new List<SensorValue>();
        var notifications = new List<SensorValueNotificationDto>();
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
            
            var map = mappedItems.First(x =>
                x.SensorId == sensorValue.SensorId &&
                x.Type == sensorValue.SensorType);
            
            notifications.Add(new SensorValueNotificationDto
            {
                SensorMapId = map.Id,
                SensorId = sensorValue.SensorId,
                SensorType = sensorValue.SensorType,
                SensorName = map.SensorName,
                Value = sensorValue.Value,
                Timestamp = item.Timestamp
            });
            
        }

        await _sensorsRepository.AddNewSensorsValuesAsync(newItems, cancellationToken);
        await StreamValuesToUsers(notifications, cancellationToken);
    }

    public async Task<List<SensorDTO>> GetAllSensorMapping(CancellationToken cancellationToken)
    {
        var sensors = await _sensorsRepository.GetSensorsMappingAsync(cancellationToken);
        return sensors.Adapt<List<SensorDTO>>();
    }

    public async Task RenameSensor(Guid id, string newName, CancellationToken cancellationToken)
    {
        await _sensorsRepository.RenameSensorAsync(id, newName, cancellationToken);
    }

    public async Task<List<SensorValue>> GetSensorsValuesAsync(Guid mappedSensorId, long startTime, long endTime, int count, CancellationToken cancellationToken)
    { 
        var hasAccess = await _userService.UserHasAccessToSensorAsync(_currentUserService.UserId.ToString(), mappedSensorId, cancellationToken);
        if(!hasAccess) throw new Exception("User not authorized");
        return await _sensorsRepository.GetSensorsValuesAsync(mappedSensorId, startTime, endTime, count, cancellationToken);
    }

    private async Task ValidateSensorsAsync(List<BaseSensorValueKafkaDTO> sensorValues, CancellationToken cancellationToken)
    {
        var mappedItems = await _sensorsRepository.GetSensorsMappingAsync(cancellationToken);
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
            await _sensorsRepository.AddNewSensorsMappingAsync(newItems, cancellationToken);
        }
    }
    
    private async Task StreamValuesToUsers(List<SensorValueNotificationDto> notifications, CancellationToken cancellationToken)
    {
        foreach (var group in notifications.GroupBy(x => x.SensorMapId))
        {
            await _realtimeNotifier.NotifySensorValuesAsync(group.Key, group.ToList());
        }
    }
}
