using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;

namespace Application.Services.Abstract.User;

public interface IUserService
{
    Task<UserDTO> GetMe(Guid userId, CancellationToken cancellationToken = default);
    Task<List<SensorDTO>> GetSensorsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UserHasAccessToSensorAsync(string userId, Guid sensorId, CancellationToken cancellationToken = default);
}
