using Application;
using Application.Repositories.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace MainApp.SignalR;

public class SensorHub : Hub
{
    private readonly IUserRepository _userRepository;

    public SensorHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(CustomClaims.UserId)?.Value;
        var sensors = await _userRepository.GetUserSensors(userId);
        foreach (var sensor in sensors)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sensor.Id.ToString());
        }
        await base.OnConnectedAsync();
    }
    
}
