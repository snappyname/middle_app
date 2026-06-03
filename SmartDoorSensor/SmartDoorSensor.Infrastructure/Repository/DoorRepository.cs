using Domain.Enums;
using Infrastructure.Repository.Abstract;
using System.Text.Json;

namespace Infrastructure.Repository;

public class DoorRepository : IDoorRepository
{
    private readonly string _filePath;

    public DoorRepository()
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "door.json");
    }

    public async Task<DoorStatusType> GetDoorStatus()
    {
        if (!File.Exists(_filePath))
        {
            return DoorStatusType.Closed;
        }

        var json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return DoorStatusType.Closed;
        }

        try
        {
            return JsonSerializer.Deserialize<DoorStatusType>(json);
        }
        catch
        {
            return DoorStatusType.Closed;
        }
    }

    public async Task SetDoorStatus(DoorStatusType newStatus)
    {
        try
        {
            var json = JsonSerializer.Serialize(newStatus);

            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
