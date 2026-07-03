namespace Contracts.Frontend.SignalR;

public class BroadcastMessageModel<T>
{
    public string Type { get; set; }
    public T Payload { get; set; }
}
