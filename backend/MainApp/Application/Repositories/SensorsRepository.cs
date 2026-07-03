using Application.Repositories.Abstract;
using DAL;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Repositories;

public class SensorsRepository : ISensorsRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "SensorsMap";
    
    public SensorsRepository(
        AppDbContext dbContext,
        IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<List<SensorsMap>> GetSensorsMappingAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SensorsMap.ToListAsync(cancellationToken);
    }

    public async Task AddNewSensorsMappingAsync(List<SensorsMap> sensorsMaps, CancellationToken cancellationToken)
    {
       await _dbContext.SensorsMap.AddRangeAsync(sensorsMaps, cancellationToken);
       await _dbContext.SaveChangesAsync(cancellationToken);
       _cache.Remove(CacheKey);
    }

    public async Task AddNewSensorsValuesAsync(List<SensorValue> values, CancellationToken cancellationToken)
    {
        await _dbContext.SensorValues.AddRangeAsync(values, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsSensorExist(SensorType sensorType, long sensorId, CancellationToken cancellationToken)
    {
       var allSensors = await GetSensorsMappingAsync(cancellationToken);
       return allSensors.Any(x => x.SensorId == sensorId && x.Type == sensorType);
    }

    public async Task<Guid> AddNewSensor(SensorType sensorType, long sensorId, string sensorName, CancellationToken cancellationToken)
    {
        var sensor = new SensorsMap { SensorId = sensorId, Type = sensorType, SensorName = sensorName };
        await _dbContext.AddAsync(sensor, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKey);
        return sensor.Id;
    }

    public async Task RenameSensorAsync(Guid mappedSensorId, string sensorName, CancellationToken cancellationToken)
    {
        var items =  await _dbContext.SensorsMap.ToListAsync(cancellationToken);
        var sensor = items.FirstOrDefault(x => x.Id == mappedSensorId);
        if(sensor == null) throw new Exception($"Sensor {mappedSensorId} not found");
        sensor.SensorName = sensorName;
        await _dbContext.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKey);
    }

    public async Task<List<SensorValue>> GetSensorsValuesAsync(Guid mappedSensorId, long startTime, long endTime, int count, CancellationToken cancellationToken)
    {
        var query = await _dbContext.SensorValues
            .Where(x => x.SensorsMapId == mappedSensorId
                        && x.Timestamp >= startTime
                        && x.Timestamp <= endTime)
            .OrderBy(x => x.Timestamp).ToListAsync(cancellationToken);

        if (query.Count <= count)
            return query;

        double step = (double)query.Count / count;

        var result = Enumerable.Range(0, count)
            .Select(i => query[(int)Math.Floor(i * step)])
            .ToList();

        return result;
    }

    public async Task<List<User>> GetUsersBySensorsAsync(Guid mappedSensorId, CancellationToken cancellationToken)
    {
        return await _dbContext.SensorsMap
            .Where(x => x.Id == mappedSensorId)
            .SelectMany(x => x.Users)
            .ToListAsync(cancellationToken);
    }
}
