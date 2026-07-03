using Application.Services.Abstract.Sensors;
using Contracts.Kafka;
using KafkaFlow;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.KafkaHandlers;

public class TemperatureHandler : IMessageHandler<List<TemperatureValueKafkaDTO>>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public TemperatureHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Handle(IMessageContext context, List<TemperatureValueKafkaDTO> message)
    {
        using var scope = _scopeFactory.CreateScope();
        var sensorsService = scope.ServiceProvider.GetRequiredService<ISensorsService>();
        
        await sensorsService.LogSensorsValuesAsync(message.Adapt<List<BaseSensorValueKafkaDTO>>());
    }
}
