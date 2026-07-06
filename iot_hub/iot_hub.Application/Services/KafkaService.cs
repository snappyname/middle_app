using Application.Services.Abstract;
using Confluent.Kafka;
using Contracts.DTO;
using Domain;
using Domain.Enums;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ILogger<KafkaService> _logger;

        private readonly IProducer<string, string> _producer;

        public KafkaService(ILogger<KafkaService> logger, IConfiguration configuration)
        {
            _logger = logger;
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaUrl"]!,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendNewValueAsync<T>(IEnumerable<SensorValue<T>> value)
        {
            var items = value.ToList();

            if (items.Count == 0)
            {
                return;
            }
            var sensorType = items[0].SensorType;
            if (items.Any(x => x.SensorType != sensorType))
            {
                throw new InvalidOperationException("All sensor values must have the same SensorType.");
            }

            await SendMessageAsync(sensorType.ToString(), GetDtoSensorJson(items));
        }

        public async Task SendNewValueAsync<T>(SensorValue<T> value)
        {
            await SendNewValueAsync(new[] { value });
        }

        private async Task SendMessageAsync(string topic, string payload)
        {
            var result = await _producer.ProduceAsync(
                topic,
                new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = payload
                });

            _logger.LogInformation("Kafka message sent to {TopicPartitionOffset}", result.TopicPartitionOffset);
        }
        
        private string GetDtoSensorJson<T>(List<SensorValue<T>> value)
        {
            switch (value[0].SensorType)
            {
                case SensorType.Humidity:
                    return JsonSerializer.Serialize(value.Adapt<List<HumidityValueDTO>>());
                case SensorType.Temperature:
                    return JsonSerializer.Serialize(value.Adapt<List<TemperatureValueDTO>>());
                case SensorType.SmartDoor:
                    return JsonSerializer.Serialize(value.Adapt<List<SmartDoorValueDTO>>());
            }

            throw new NotSupportedException(
                $"Sensor type {value.GetType().Name} is not supported");
        }
    }
}
