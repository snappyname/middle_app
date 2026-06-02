using Application.Services.Abstract;
using Confluent.Kafka;
using Contracts.DTO;
using Domain;
using Domain.Enums;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ILogger<KafkaService> _logger;

        public KafkaService(ILogger<KafkaService> logger)
        {
            _logger = logger;
        }

        public async Task SendNewValue<T>(SensorValue<T> value)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:29092", Acks = Acks.All };

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
