using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;

namespace Application.Services.Abstract.Admin;

public interface IAdminService
{
    Task<List<UserDTO>> GetAllUsersWithSensorsAsync(CancellationToken cancellationToken =  default);
    Task<Guid> AddNewSensorAsync(SensorDTO sensor, CancellationToken cancellationToken =  default);
    Task<List<SensorDTO>> GetAllSensorsAsync(CancellationToken cancellationToken =  default);
    Task UpdateUserAsync(Guid id, string name, bool isAdmin = false, CancellationToken cancellationToken =  default);
    Task UpdateUserSensorsAsync(List<SensorDTO> sensorValues, CancellationToken cancellationToken =  default);
}
