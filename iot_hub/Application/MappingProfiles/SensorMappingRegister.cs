using Contracts.DTO;
using Contracts.DTO.Input;
using Domain;
using Mapster;

namespace Application.MappingProfiles
{
    public class SensorMappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SmartDoorValue, SmartDoorValueDTO>()
                .Map(dest => dest.Value, src => src.Value)
                .Map(dest => dest.Timestamp,
                    src => DateTimeOffset
                        .FromUnixTimeMilliseconds(src.Timestamp)
                        .ToString("O"));
            config.NewConfig<SmartDootInputStateDTO, SmartDoorValue>()
                .Map(dest => dest.Value, src => src.Status)
                .Map(dest => dest.Timestamp, src => src.Timestamp);
        }
    }
}
