using Domain;

namespace Application.Services.Abstract
{
    public interface IKafkaService
    {
        Task SendNewValueAsync<T>(IEnumerable<SensorValue<T>> value);
        Task SendNewValueAsync<T>(SensorValue<T> value);
    }
}
