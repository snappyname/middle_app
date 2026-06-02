using Application.Services.Abstract;
using Contracts.DTO.Input;
using Coravel.Invocable;
using Domain;
using Domain.Enums;
using Mapster;
using System.Text.Json;

namespace iot_hub.BackgroundJobs;

public class GetDoorDataJob : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GetDoorDataJob> _logger;
    private readonly IKafkaService _kafkaService;

    public GetDoorDataJob(IHttpClientFactory factory, ILogger<GetDoorDataJob> logger, IKafkaService kafkaService)
    {
        _httpClient = factory.CreateClient();
        _logger = logger;
        _kafkaService = kafkaService;
    }

    public async Task Invoke()
    {
        var response = await _httpClient.GetAsync("http://localhost:8080/api/SmartDoor");
        var result = JsonSerializer.Deserialize<SmartDootInputStateDTO>(await response.Content.ReadAsStringAsync()).Adapt<SmartDoorValue>();
        await _kafkaService.SendNewValue(result);
    }
}
