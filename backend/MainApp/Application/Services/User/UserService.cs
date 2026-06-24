using Application.Repositories.Abstract;
using Application.Services.Abstract;
using Application.Services.Abstract.User;
using Contracts.Frontend.Admin;
using Mapster;

namespace Application.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService( IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> GetMe(Guid userId)
    {
        var user = await _userRepository.GetById(userId);
        if(user == null) throw new Exception("User not found");
        return user.Adapt<UserDTO>();
    }
}
