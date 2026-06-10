using Domain;

namespace Application.Services.Abstract;

public interface IUserService
{
    Task<User> GetMe(string userId);
}
