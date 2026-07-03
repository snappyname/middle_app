using Domain;

namespace Application.Repositories.Abstract;

public interface IUserRepository
{
    Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserWithSensorsById(string id, CancellationToken cancellationToken = default);
    Task<User?> GetByGithubId(long id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<User> SetUserGithubId(User user, long githubId, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsersWithSensors(CancellationToken cancellationToken = default);
    Task<List<SensorsMap>> GetUserSensors(string userId, CancellationToken cancellationToken = default);
    Task UpdateUser(User user, CancellationToken cancellationToken = default);
}
