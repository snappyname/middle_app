using Domain;

namespace Application.Repositories.Abstract;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByGithubId(long id);
    Task<User?> GetByEmail(string email);
    Task<User> SetUserGithubId(User user, long githubId);
    Task<List<User>> GetAllUsers();
    Task UpdateUser(User user);
    
}
