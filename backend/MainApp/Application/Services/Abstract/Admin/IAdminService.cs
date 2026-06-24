using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;

namespace Application.Services.Abstract.Admin;

public interface IAdminService
{
    Task<List<UserDTO>> GetAllUsersAsync();
    Task<Guid> AddNewSensorAsync(SensorDTO sensor);
    Task<List<SensorDTO>> GetAllSensorsAsync();
    Task UpdateUserAsync(Guid id, string name, bool isAdmin = false);
}
