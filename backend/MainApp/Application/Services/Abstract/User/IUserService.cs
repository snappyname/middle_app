using Contracts.Frontend.Admin;

namespace Application.Services.Abstract.User;

public interface IUserService
{
    Task<UserDTO> GetMe(Guid userId);
}
