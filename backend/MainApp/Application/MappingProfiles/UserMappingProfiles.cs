using Contracts.Frontend.Admin;
using Domain;
using Mapster;

namespace Application.MappingProfiles;

public class UserMappingProfiles : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDTO, User>()
            .Map(dest => dest.IsAdmin, src => src.IsAdmin)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Id, src => src.Id);

        config.NewConfig<User, UserDTO>()
            .Map(dest => dest.IsAdmin, src => src.IsAdmin)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Username, src => src.UserName)
            .Map(dest => dest.Id, src => src.Id);
    }
}
