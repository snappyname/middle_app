using Contracts.Kafka;
using Domain.Enums;
using Mapster;

namespace Application.MappingProfiles
{
    public class KafkaDTOMappingProfilesIRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HumidityValueKafkaDTO, BaseSensorValueKafkaDTO>()
                .Map(dest => dest.SensorId, src => src.SensorId)
                .Map(dest => dest.Timestamp, src => src.Timestamp)
                .Map(dest => dest.Value, src => src.Value)
                .Map(dest => dest.SensorType, src => SensorType.Humidity);
            
       config.NewConfig<TemperatureValueKafkaDTO, BaseSensorValueKafkaDTO>()
                .Map(dest => dest.SensorId, src => src.SensorId)
                .Map(dest => dest.Timestamp, src => src.Timestamp)
                .Map(dest => dest.Value, src => src.Value)
                .Map(dest => dest.SensorType, src => SensorType.Temperature);
        }
    }
}
