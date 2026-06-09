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
        private readonly IConfiguration _configuration;

        public KafkaService(ILogger<KafkaService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendNewValueAsync<T>(IEnumerable<SensorValue<T>> value)
        {
            var items = value.ToList();
            if (!items.Any())
            {
                return;
            }
            var config = new ProducerConfig { BootstrapServers = _configuration["KafkaUrl"]!, Acks = Acks.All };
            
            using var producer = new ProducerBuilder<string, string>(config).Build();

            var result = await producer.ProduceAsync(
                items[0].SensorType.ToString(),
                new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = GetDtoSensorJson(items)
                });
            _logger.LogInformation(result.TopicPartitionOffset.ToString());
        }

        public async Task SendNewValueAsync<T>(SensorValue<T> value)
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
        
        private string GetDtoSensorJson<T>(SensorValue<T> value)
        {
            switch (value.SensorType)
            {
                case SensorType.Humidity:
                    return JsonSerializer.Serialize(value.Adapt<HumidityValueDTO>());
                case SensorType.Temperature:
                    return JsonSerializer.Serialize(value.Adapt<TemperatureValueDTO>());
                case SensorType.SmartDoor:
                    return JsonSerializer.Serialize(value.Adapt<SmartDoorValueDTO>());
            }

            throw new NotSupportedException(
                $"Sensor type {value.GetType().Name} is not supported");
        }
    }
}
