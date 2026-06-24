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

    public async Task<List<SensorsMap>> GetSensorsMappingAsync()
    {
        return await _cache.GetOrCreateAsync(
            CacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _dbContext.SensorsMap.ToListAsync();
            }) ?? [];
    }

    public async Task AddNewSensorsMappingAsync(List<SensorsMap> sensorsMaps)
    {
       await _dbContext.SensorsMap.AddRangeAsync(sensorsMaps);
       await _dbContext.SaveChangesAsync();
       _cache.Remove(CacheKey);
    }

    public async Task AddNewSensorsValuesAsync(List<SensorValue> values)
    {
        await _dbContext.SensorValues.AddRangeAsync(values);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsSensorExist(SensorType sensorType, long sensorId)
    {
       var allSensors = await GetSensorsMappingAsync();
       return allSensors.Any(x => x.SensorId == sensorId && x.Type == sensorType);
    }

    public async Task<Guid> AddNewSensor(SensorType sensorType, long sensorId, string sensorName)
    {
        var sensor = new SensorsMap { SensorId = sensorId, Type = sensorType, SensorName = sensorName };
        await _dbContext.AddAsync(sensor);
        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey);
        return sensor.Id;
    }

    public async Task RenameSensor(Guid mappedSensorId, string sensorName)
    {
        var items =  await _dbContext.SensorsMap.ToListAsync();
        var sensor = items.FirstOrDefault(x => x.Id == mappedSensorId);
        if(sensor == null) throw new Exception($"Sensor {mappedSensorId} not found");
        sensor.SensorName = sensorName;
        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey);
    }
}
