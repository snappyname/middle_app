using Application.Repositories.Abstract;
using Application.Services.Abstract.Admin;
using Application.Services.Abstract.Auth;
using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;
using Mapster;
namespace Application.Services.Admin;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly ISensorsRepository _sensorsRepository;
    private readonly ICurrentUserService _currentUserService;

    public AdminService(IUserRepository userRepository, ISensorsRepository sensorsRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _sensorsRepository = sensorsRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsers();
        return users.Adapt<List<UserDTO>>();
    }

    public async Task<Guid> AddNewSensorAsync(SensorDTO sensor)
    {
        var isExists = await _sensorsRepository.IsSensorExist(sensor.SensorType, sensor.SensorId);
        if (isExists) throw new Exception("Sensor already exists");
        return await _sensorsRepository.AddNewSensor(sensor.SensorType, sensor.SensorId, sensor.SensorName);
    }

    public async Task<List<SensorDTO>> GetAllSensorsAsync()
    {
        var sensors = await _sensorsRepository.GetSensorsMappingAsync();
        return sensors.Adapt<List<SensorDTO>>();
    }

    public async Task UpdateUserAsync(Guid id, string name, bool isAdmin = false)
    {
         var user = await _userRepository.GetById(id);
         if (user == null) throw new Exception();
         if (_currentUserService.UserId != id)
         {
               user.IsAdmin = isAdmin;
         }
       
         user.UserName = name;
         await _userRepository.UpdateUser(user);
    }
}
