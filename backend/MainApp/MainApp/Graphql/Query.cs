using Application.Services.Abstract.Sensors;
using Contracts.Frontend.SignalR;
using Mapster;

namespace MainApp.Graphql;

public class Query
{
    [HotChocolate.Authorization.Authorize]
    public async Task<IEnumerable<SensorValueDTO>> GetSensorValues(
        Guid sensorId,
        long startTime,
        long endTime,
        int count,
        [Service] ISensorsService sensorsService)
    {
        var value = await sensorsService.GetSensorsValuesAsync(sensorId, startTime, endTime, count);
        return value.Adapt<List<SensorValueDTO>>();
    }
}
