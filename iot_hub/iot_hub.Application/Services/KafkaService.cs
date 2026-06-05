using Application.Services.Abstract;
using Confluent.Kafka;
using Contracts.DTO;
using Domain;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ILogger<KafkaService> _logger;
        private readonly IConfiguration _configuration;

        public KafkaService(ILogger<KafkaService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendNewValue<T>(SensorValue<T> value)
        {
            var config = new ProducerConfig { BootstrapServers = _configuration["KafkaUrl"]!, Acks = Acks.All };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            var result = await producer.ProduceAsync(
                value.SensorType.ToString(),
                new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = GetDtoSensorJson(value)
                });
            _logger.LogInformation(result.TopicPartitionOffset.ToString());
        }
        
        private string GetDtoSensorJson<T>(SensorValue<T> value)
        {
            return value switch
            {
                SmartDoorValue door =>
                    JsonSerializer.Serialize(door.Adapt<SmartDoorValueDTO>()),

                HumidityValue humidity =>
                    JsonSerializer.Serialize(humidity.Adapt<HumidityValueDTO>()),

                TemperatureValue temperature =>
                    JsonSerializer.Serialize(temperature.Adapt<TemperatureValueDTO>()),

                _ => throw new NotSupportedException(
                    $"Sensor type {value.GetType().Name} is not supported")
            };
        }
    }
}
