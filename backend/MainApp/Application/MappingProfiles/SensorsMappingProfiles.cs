using Contracts.Frontend.Sensors;
using Contracts.Frontend.SignalR;
using Domain;
using Mapster;

namespace Application.MappingProfiles;

public class SensorsMappingProfiles : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SensorDTO, SensorsMap>()
            .Map(dest => dest.SensorName, src => src.SensorName)
            .Map(dest => dest.Type, src => src.SensorType)
            .Map(dest => dest.Id, src => src.MappedSensorId)
            .Map(dest => dest.SensorId, src => src.SensorId);

        config.NewConfig<SensorsMap, SensorDTO>()
            .Map(dest => dest.SensorName, src => src.SensorName)
            .Map(dest => dest.SensorType, src => src.Type)
            .Map(dest => dest.MappedSensorId, src => src.Id)
            .Map(dest => dest.SensorId, src => src.SensorId);

        config.NewConfig<SensorValue, SensorValueDTO>()
            .Map(dest => dest.Timestamp, src => src.Timestamp)
            .Map(dest => dest.SensorId, src => src.SensorsMapId)
            .Map(dest => dest.Value, src => src.Value);    
        
        config.NewConfig<SensorValueDTO, SensorValue>()
            .Map(dest => dest.Timestamp, src => src.Timestamp)
            .Map(dest => dest.SensorsMapId, src => src.SensorId)
            .Map(dest => dest.Value, src => src.Value);
    }
}
