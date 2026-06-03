using Domain;

namespace Application.Services.Abstract
{
    public interface IKafkaService
    {
        Task SendNewValue<T>(SensorValue<T> value);
    }
}
