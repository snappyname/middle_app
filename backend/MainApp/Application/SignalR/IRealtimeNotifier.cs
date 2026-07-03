namespace Application.SignalR;

public interface IRealtimeNotifier
{
    Task NotifySensorValuesAsync<T>(Guid sensorMapId, T payload);
}
