using Application.Services.Abstract.Sensors;
using Contracts.Kafka;
using KafkaFlow;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.KafkaHandlers;

public class HumidityHandler : IMessageHandler<List<HumidityValueKafkaDTO>>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public HumidityHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Handle(IMessageContext context, List<HumidityValueKafkaDTO> message)
    {
        using var scope = _scopeFactory.CreateScope();
        var sensorsService = scope.ServiceProvider.GetRequiredService<ISensorsService>();

        await sensorsService.LogSensorsValuesAsync(message.Adapt<List<BaseSensorValueKafkaDTO>>());
    }
}
