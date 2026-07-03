using Application.Repositories.Abstract;
using Application.Services.Abstract;
using Application.Services.Abstract.User;
using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;
using Mapster;

namespace Application.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> GetMe(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(userId, cancellationToken);
        if(user == null) throw new Exception("User not found");
        return user.Adapt<UserDTO>();
    }

    public async Task<List<SensorDTO>> GetSensorsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var sensorsMap = await _userRepository.GetUserSensors(userId.ToString(), cancellationToken);
        return sensorsMap.Adapt<List<SensorDTO>>();
    }

    public async Task<bool> UserHasAccessToSensorAsync(string userId, Guid sensorId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithSensorsById(userId, cancellationToken);
        if(user.IsAdmin) return true;
        return user.Sensors.Select(x => x.Id).Contains(sensorId);
    }
}
