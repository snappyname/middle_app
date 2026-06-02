using Coravel.Invocable;

namespace iot_hub.BackgroundJobs;

public class GetHumidityDataJob : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GetHumidityDataJob> _logger;
    
    public GetHumidityDataJob(IHttpClientFactory factory, ILogger<GetHumidityDataJob> logger)
    {
        _httpClient = factory.CreateClient();
        _logger = logger;
    }

    public async Task Invoke()
    {
        try
        {
            //var response = await _httpClient.GetAsync("http://localhost:7080/api/Humidity");
            //_logger.LogInformation($"GetHumidityDataJob response: {await response.Content.ReadAsStringAsync()}");
        }
        catch
        {
            // ignored
        }
    }
}
