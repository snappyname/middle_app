using Application.SignalR;
using Contracts.Frontend.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace MainApp.SignalR;

public class SignalRNotifier : IRealtimeNotifier
{
    private readonly IHubContext<SensorHub> _hub;
    private const string BroadcastMethod = "broadcast";

    public SignalRNotifier(IHubContext<SensorHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifySensorValuesAsync<T>(Guid sensorMapId, T payload)
    {
        var message = new BroadcastMessageModel<T>() { Type = SignalRMessageNames.SensorUpdated, Payload = payload };
        await _hub.Clients.Group(sensorMapId.ToString()).SendAsync(BroadcastMethod, message);
    }
}
